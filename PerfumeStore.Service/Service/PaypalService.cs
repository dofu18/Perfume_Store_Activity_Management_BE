using Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PerfumeStore.Repository.Model;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using PerfumeStore.Repository;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Service.BusinessModel;
using ApplicationContext = PayPalCheckoutSdk.Orders.ApplicationContext;
using Microsoft.AspNetCore.Http;
using PerfumeStore.Service.BusinessModel.CustomResponse;
using NuGet.Protocol;
using PerfumeStore.Service.Service;


namespace PerfumeStore.Service.Service
{
    public class PaypalService : BaseService
    {
        private readonly PayPalHttpClient _client;
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<PaypalService> _logger;
        private readonly OrderService _orderService;

        public PaypalService(PerfumeStoreContext dbContext, OrderService orderService, IConfiguration configuration, UnitOfWork unitOfWork, IHttpContextAccessor httpCtx, ILogger<PaypalService> logger) : base(httpCtx, dbContext)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

            var clientId = configuration["PayPal:ClientId"];
            var clientSecret = configuration["PayPal:ClientSecret"];
            PayPalEnvironment environment;

            if (configuration["PayPal:Environment"] == "live")
            {
                environment = new LiveEnvironment(clientId, clientSecret);
            }
            else
            {
                environment = new SandboxEnvironment(clientId, clientSecret);
            }

            _client = new PayPalHttpClient(environment);
        }

        // Method to create a PayPal order
        public async Task<IActionResult> CreateOrder(CreateOrderRequest req, Guid userId, string redirectUrl)
        {
            // Check if the user exists
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return ErrorResp.Unauthorized("Invalid token");
            }

            // Optionally validate perfume products in the order (omitted here for brevity)

            var request = new OrdersCreateRequest();
            var amount = req.Amount;
            var returnUrl = $"{_httpCtx.HttpContext?.Request.Scheme}://{_httpCtx.HttpContext?.Request.Host}/api/v1/paypal/capture-order?userId={userId}&redirectUrl={redirectUrl}";
            if (req.OrderId.HasValue && req.OrderId != Guid.Empty)
            {
                var order = _unitOfWork.Orders.GetById((Guid)req.OrderId);
                if (order == null)
                {
                    return ErrorResp.NotFound("Order not found");
                }
                amount = order.TotalAmount;
                returnUrl += $"&orderId={req.OrderId}";
            }

            request.Prefer("return=representation");

            //var returnUrl = "";

            request.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = req.Currency,
                            Value = req.Amount.ToString()  // Set the order amount from request
                        }
                    }
                },
                ApplicationContext = new ApplicationContext
                {
                    BrandName = "PerfumeStore",
                    LandingPage = "BILLING",
                    CancelUrl = "https://www.example.com",
                    ReturnUrl = returnUrl,
                    UserAction = "CONTINUE",
                }
            });

            try
            {
                var response = await _client.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();
                _logger.LogInformation($"PayPal order created successfully: {result.Id}");
                return SuccessResp.Ok(result.ToJson());
            }
            catch (HttpException ex)
            {
                _logger.LogError($"PayPal API Error: {ex.Message}");
                throw;
            }
        }

        // Method to capture the PayPal order
        public async Task<IActionResult> CaptureOrder(string token, string payerId, Guid userId, Guid orderId, string redirectUrl)
        {
            var request = new OrdersCaptureRequest(token);
            request.RequestBody(new OrderActionRequest());

            // check is order already captured
            var req = new OrdersGetRequest(token);
            var resp = await _client.Execute(req);
            var order = resp.Result<PayPalCheckoutSdk.Orders.Order>();
            if (order.Status == "COMPLETED")
            {
                return SuccessResp.Redirect(redirectUrl);
            }

            try
            {
                var response = await _client.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

                if (result.Status != "COMPLETED")
                {
                    _logger.LogError("Failed to capture PayPal order.");
                    return ErrorResp.BadRequest("Failed to capture PayPal order.");
                }

                var amountValue = result.PurchaseUnits[0].Payments.Captures[0].Amount.Value;
                if (amountValue == null)
                {
                    _logger.LogError("Invalid PayPal response. Amount value is null.");
                    return ErrorResp.BadRequest("Invalid response from PayPal. Amount value is null.");
                }

                if (result.Status == "COMPLETED")
                {
                    await Task.Run(async () =>
                    {
                        var transaction = new Repository.Model.Transaction
                        {
                            TransactionId = Guid.NewGuid(),
                            OrderId = orderId,
                            //OrderId = (Guid)orderId,
                            PaymentMethod = "Paypal",
                            PaymentStatus = "Success",
                            TransactionDate = DateTime.Now
                        };

                        _dbContext.Transactions.Add(transaction);
                        await _dbContext.SaveChangesAsync();
                    });
                }

                if (orderId != Guid.Empty)
                {
                    await Task.Run(async () =>
                    {
                        await _orderService.HandleCompleteOrder((Guid)orderId, userId);
                    });
                }

                // Process the payment (e.g., save to DB)
                await ProcessPayment(userId, Convert.ToDecimal(amountValue), result.Id);

                _logger.LogInformation($"PayPal order captured successfully: {result.Id}");
                return SuccessResp.Redirect(redirectUrl);
            }
            catch (HttpException ex)
            {
                _logger.LogError($"PayPal API Error: {ex.Message}");
                throw;
            }
        }

        // Helper method to process the payment and update the DB
        private async Task ProcessPayment(Guid userId, decimal amount, string paypalOrderId)
        {
            // Assuming `Order` and `Transaction` entities are part of your project
            var transaction = new Repository.Model.Transaction
            {
                TransactionId = Guid.NewGuid(),
                PaymentMethod = "PayPal",
                PaymentStatus = "Success",
                OrderId = Guid.Parse(paypalOrderId),
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.Transactions.Create(transaction);
            await _unitOfWork.SaveChangesAsync();

            // Optionally, update the user's cart or order status
        }
    }
}
