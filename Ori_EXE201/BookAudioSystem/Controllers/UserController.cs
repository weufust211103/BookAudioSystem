using BookAudioSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAudioSystem.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize] // Ensure the user is authenticated
        [HttpPost("change-role-to-owner")]
        public async Task<IActionResult> ChangeUserRoleToOwner()
        {
            try
            {
                // Get the email from the JWT token claims
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(new { error = "User email not found in token." });
                }

                // Call the service to change the role to "Owner"
                var result = await _userService.ChangeUserRoleToOwnerByEmailAsync(email);
                if (result)
                {
                    return Ok(new { message = "User role changed to Owner successfully." });
                }

                return BadRequest(new { error = "Failed to change user role." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
