
using TDV.Entity.Entities.Authentications;

namespace TDV.Application.Shared.Authentications
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string username);
        RefreshToken GenerateRefreshToken();
        Task SaveRefreshTokenAsync(RefreshToken token);
    }
}
