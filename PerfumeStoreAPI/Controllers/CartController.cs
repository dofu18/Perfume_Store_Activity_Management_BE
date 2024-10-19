using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(Guid userId, Guid perfumeId, int quantity)
        {
            try
            {
                var success = await _cartService.AddToCartAsync(userId, perfumeId, quantity);

                return Ok("Item successfully added to cart.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var cartItems = await _cartService.GetCartByUserIdAsync_Optimized(userId);

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("Cart is empty or user not found.");
            }

            return Ok(cartItems);
        }
    }
}
