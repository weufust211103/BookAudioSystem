using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookAudioSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            // Get the JWT token from the request's authorization header
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Token is missing or invalid.");
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return Unauthorized("Invalid token.");
                }

                // Extract claims (e.g., name, email, role) from the token
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
                var email = _userService.GetEmailByUsername(username);
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                // Ensure the claims are valid
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
                {
                    return Unauthorized("Token claims are missing or invalid.");
                }

                // Create user info model
                var user = new UserModel
                {
                    Username = username,
                    Email = email,
                    Role = role
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized($"Error processing token: {ex.Message}");
            }
        }
    }
}
