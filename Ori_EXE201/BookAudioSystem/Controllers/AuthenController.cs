using BookAudioSystem.BusinessObjects.Models;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalBook.BusinessObjects.Models;
using System.Security.Claims;

namespace BookAudioSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registerModel">The registration model.</param>
        /// <returns>The user response.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userResponse = await _userService.RegisterAsync(registerModel);
                return Ok(userResponse); // Return success response
            }
            catch (Exception ex)
            {
                // Return error response
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Login with user credentials.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>The user response.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userResponse = await _userService.LoginAsync(loginModel);
                return Ok(userResponse); // Return success response
            }
            catch (Exception ex)
            {
                // Return error response
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <returns>The user information.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserResDto>> GetUserInfo()
        {
            // Extract the email from the JWT token claims
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
            {
                return Unauthorized();
            }

            var email = emailClaim.Value;

            // Use the service to retrieve the user info by email
            var userInfo = await _userService.GetUserInfoByEmailAsync(email);

            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }
    }
}

