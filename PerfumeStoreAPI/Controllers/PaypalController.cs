using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using PerfumeStore.API.RequestModel;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [ApiController]
    [Route("api/v1/paypal")]
    public class PaypalController : ControllerBase
    {
        private readonly PaypalService _paypalService;

        private readonly ILogger _logger;

        public PaypalController(PaypalService paypalService, ILogger<PaypalController> logger)
        {
            _logger = logger;
            _paypalService = paypalService;
        }

        //[HttpPost("create-order")]
        //public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel request, Guid userId, string redirectUrl)
        //{
        //    try
        //    {
        //        return await _paypalService.CreateOrder(request, userId, redirectUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = "Failed to create order", details = ex.Message });
        //    }
        //}

        //[HttpGet("capture-order")]
        //public async Task<IActionResult> CaptureOrder([FromQuery] string token, string PayerId, Guid userId, Guid orderId, string redirectUrl)
        //{
        //    try
        //    {
        //        //var order = await _payPalService.CaptureOrder(token, PayerId);
        //        //return Ok(order.ToJson());

        //        _logger.LogInformation("Capture Order");
        //        return await _paypalService.CaptureOrder(token, PayerId, userId, orderId, redirectUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = "Failed to capture order", details = ex.Message });
        //    }
        //}

        // API to create a PayPal order
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, Guid userId, string redirectUrl)
        {
            //var result = await _paypalService.CreateOrder(request, userId, redirectUrl);
            //if (!result.Success)
            //{
            //    return BadRequest(result.Message);
            //}
            //return Ok(result.Data); // Return PayPal order ID or approval URL   

            try
            {
                _logger.LogInformation("Create Paypal order");
                return await _paypalService.CreateOrder(request, userId, redirectUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create order", details = ex.Message });
            }
        }

        // API to capture a PayPal order
        [HttpPost("capture-order")]
        public async Task<IActionResult> CaptureOrder(string token, string PayerID, Guid userId, Guid orderId, string redirectUrl)
        {
            //var result = await _paypalService.CaptureOrder(token, payerId, userId, redirectUrl);
            //if (!result.Success)
            //{
            //    return BadRequest(result.Message);
            //}
            //return Ok(result.Message); // Return success message

            try
            {
                //var order = await _payPalService.CaptureOrder(token, PayerId);
                //return Ok(order.ToJson());

                _logger.LogInformation("Capture Order");
                return await _paypalService.CaptureOrder(token, PayerID, userId, orderId, redirectUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to capture order", details = ex.Message });
            }
        }
    }
}
