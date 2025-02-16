using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace cine_hub_server.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public JwtService(IConfiguration configuration, UserManager<User> userManager, JwtOptions jwtOptions)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }

        public string CreateToken(IEnumerable<Claim> claims, int lifetimeMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(lifetimeMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateAccessToken(User user)
        {
            var claims = GetClaims(user);
            return CreateToken(claims, _jwtOptions.AccessLifetime);
        }

        public string CreateRefreshToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return CreateToken(claims, _jwtOptions.RefreshLifetime);
        }

        public IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString("yyyy-MM-dd"))
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            return claims;
        }
    }
}
