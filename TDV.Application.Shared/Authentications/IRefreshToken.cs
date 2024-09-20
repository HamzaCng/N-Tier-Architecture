using TDV.DataAccess.Abstract;
using TDV.Entity.Entities.Authentications;

namespace TDV.Application.Shared.Authentications
{
    public interface IRefreshToken : IRepository<RefreshToken>
    {
        Task SaveRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
    }
}
