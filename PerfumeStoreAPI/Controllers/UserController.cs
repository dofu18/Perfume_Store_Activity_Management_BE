using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
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
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                Phone = user.Phone,
                ProfileUrl = user.ProfileUrl,
                Metadata = user.Metadata,
                Status = user.Status,
                LastLogin = user.LastLogin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DateCreated = user.DateCreated,
            };

            return Ok(response);
        }

        [HttpGet("users/admin")]
        public IActionResult GetAllUsers([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool desc, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Get paginated result from the service
            var users = _userService.GetAllUsers(search, sortBy, desc, page, pageSize);
            return Ok(users);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequestModel req)
        {
            var userModel = new UpdateUserModel
            {
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Phone = req.Phone,
                ProfileUrl = req.ProfileUrl,
                Metadata = req.Metadata,
            };

            var success = await _userService.UpdateUserAsync(id, userModel);
            if (!success) return NotFound();

            return NoContent();
        }
    }

}
