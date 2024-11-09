using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using FirebaseAdmin.Auth;
using PerfumeStore.Service.Service;
using PerfumeStore.Service.BusinessModel;

namespace PerfumeStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly AuthService _authService;

        private readonly FirebaseService _firebaseService;


        public AuthController(IConfiguration config, AuthService authService, FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
            _authService = authService;
            _config = config;
        }

        [HttpPost("verify-token")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyToken([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Unauthorized("Missing Authorization header");
            }

            // Extract the ID token from the Bearer token format
            var idToken = authorizationHeader.StartsWith("Bearer ")
                ? authorizationHeader.Substring("Bearer ".Length).Trim()
                : authorizationHeader;

            try
            {
                var decodedToken = await _firebaseService.VerifyIdTokenAsync(idToken);
                var userId = decodedToken.Uid;
                var userRecord = await _firebaseService.GetUserRecordByIdTokenAsync(idToken);

                var email = userRecord.Email;
                var name = userRecord.DisplayName ?? "Anonymous";
                var picture = userRecord.PhotoUrl ?? "";

                var existingUser =  _authService.GetUserByEmail(email);

                if (existingUser != null)
                {
                    // User exists, update the LastLogin field
                    existingUser.LastLogin = DateTime.UtcNow;
                    await _authService.UpdateUserAsync(existingUser);
                }
                else
                {
                    // User does not exist, create a new user
                    var newUser = new Repository.Model.User
                    {
                        UserId = Guid.NewGuid(),
                        Email = email,
                        FirstName = name,
                        LastName = "",
                        PasswordHash = "",
                        Role = "Customer",
                        Metadata = "",
                        Phone = "",
                        ProfileUrl = picture,
                        Status = "isActive",
                        DateCreated = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        LastLogin = DateTime.UtcNow
                    };

                    await _authService.CreateUserAsync(newUser);
                }

                return Ok(new
                {
                    userId,
                    email = userRecord.Email,
                    name = userRecord.DisplayName,
                    picture = userRecord.PhotoUrl
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid Firebase ID token.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("signin-google")]
        public IActionResult GoogleLogin([FromQuery] string redirectToMobile = "false")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri =
                        redirectToMobile == "true"
                        ? "com.perfumestorev2:/google-response"
                        : Url.Action("GoogleResponse", "Auth", null, Request.Scheme)
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result?.Principal == null)
                return BadRequest("No principal found");

            // Extract user info from Google
            var claims = result.Principal.Claims.ToList();
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required");
            }


            // You can now check if the user exists in your DB or register a new user.
            var user = await _authService.AuthenticateGoogleUser(email, name);


            // For this demo, we'll assume the user is valid and proceed with JWT token generation.

            var jwtToken = GenerateJwtToken(user);

            return Ok(new
            {
                token = jwtToken,
                email,
                name
            });
        }

        private string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //    var claims = new[]
            //    {
            //    new Claim(JwtRegisteredClaimNames.Sub, email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim("name", name),
            //    new Claim("email", email)
            //};

            var claims = new List<Claim>
    {
        new Claim("UserId", user.UserId.ToString()),
        //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim("FirstName", user.FirstName),
        new Claim("LastName", user.LastName ?? ""),
        new Claim("Email", user.Email),
        new Claim("Role", user.Role ?? ""),
        new Claim("Phone", user.Phone ?? ""),
        new Claim("ProfileUrl", user.ProfileUrl ?? ""),
        new Claim("Metadata", user.Metadata ?? ""),
        new Claim("Address", user.Address ?? ""),
        new Claim("Status", user.Status),
        //new Claim("CreatedAt", user.CreatedAt.ToString("o")),
        //new Claim("UpdatedAt", user.UpdatedAt.ToString("o")),
        new Claim("DateCreated", user.DateCreated?.ToString("o") ?? ""),
        new Claim("LastLogin", user.LastLogin?.ToString("o") ?? "")
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
