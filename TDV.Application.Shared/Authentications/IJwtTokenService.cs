
using TDV.DataAccess.Abstract;
using TDV.Entity.Entities.Authentications;

namespace TDV.Application.Shared.Authentications
{
    public interface IJwtTokenService : IRepository<RefreshToken>
    {
        string GenerateToken(string userId, string username);
        RefreshToken GenerateRefreshToken();
        Task SaveRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
    }
}
