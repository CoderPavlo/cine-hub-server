using cine_hub_server.Models;
using System.Security.Claims;

namespace cine_hub_server.Interfaces
{
    public interface IJwtService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken(User user);
        ClaimsPrincipal GetClaimsFromExpiredToken(string token);
    }
}
