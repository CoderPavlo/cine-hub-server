
using cine_hub_server.Dto;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace cine_hub_server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday,
                //Gender = model.Gender
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");
            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtService.CreateAccessToken(user);
            var refreshToken = _jwtService.CreateRefreshToken(user);

            return Ok(new { accessToken, refreshToken, role = roles.FirstOrDefault() });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtService.CreateAccessToken(user);
            var refreshToken = _jwtService.CreateRefreshToken(user);

            return Ok(new { accessToken, refreshToken, role = roles.FirstOrDefault() });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            try
            {
                var principal = _jwtService.GetClaimsFromExpiredToken(model.Token);
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                    return Unauthorized("Invalid refresh token");

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return Unauthorized("User not found");

                var roles = await _userManager.GetRolesAsync(user);
                var newAccessToken = _jwtService.CreateAccessToken(user);
                var newRefreshToken = _jwtService.CreateRefreshToken(user);

                return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken, role = roles.FirstOrDefault() });
            }
            catch
            {
                return Unauthorized("Invalid token");
            }
        }
        
        [HttpGet("check-auth")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult CheckAuth()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"Received Token: {authHeader}");
            var userClaims = User.Claims.ToList();
            foreach (var claim in userClaims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            if (!userClaims.Any())
            {
                return Unauthorized("User is not authenticated");
            }

            var usernameClaim = userClaims.FirstOrDefault(c => c.Type == "FullName");
            if (usernameClaim == null)
            {
                return Unauthorized("User name is missing in claims");
            }

            return Ok(new { message = "User is authenticated", username = usernameClaim.Value });
        }
    }
}
