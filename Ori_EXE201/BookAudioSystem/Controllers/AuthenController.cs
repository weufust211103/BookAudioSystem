using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Mvc;
using RentalBook.BusinessObjects.Models;

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

        // POST: api/authentication/register
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

        // POST: api/authentication/login
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
    }
}
