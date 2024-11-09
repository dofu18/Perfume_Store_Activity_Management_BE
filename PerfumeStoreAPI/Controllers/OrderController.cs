using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.BusinessModel.CustomResponse;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        private readonly ILogger _logger;

        public OrderController(OrderService orderService, ILogger<OrderController> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel req, Guid userId)
        {
            _logger.LogInformation("Create Order");

            //if (HttpContext.Items["payload"] is not Payload payload)
            //{
            //    return ErrorResp.Unauthorized("Invalid token");
            //}

            return await _orderService.HandleCreateOrder(req, userId);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrderByUser(Guid userId)
        {
            var activities = await _orderService.GetOrderByUserIdAsync(userId);

            if (activities == null || !activities.Any())
            {
                return NotFound("Don't have any activity or user not found.");
            }

            return Ok(activities);
        }
    }
}
