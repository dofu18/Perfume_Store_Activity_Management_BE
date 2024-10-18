using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Repository.Models;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/{id}")]
        public async Task <ActionResult<IEnumerable<UserResponseModel>>> GetUserById (Guid id)
        {
            var user = await _userService.GetUserByIdAsync (id);
            if (user == null) return NotFound();

            var response = new UserResponseModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                ProfileUrl = user.ProfileUrl,
                Metadata = user.Metadata,
                Status = user.Status,
                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };

            return Ok(response);
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool desc, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Get paginated result from the service
            var users = _userService.GetAllUsers(search, sortBy, desc, page, pageSize);
            return Ok(users);
        }
    }

}
