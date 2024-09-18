using TDV.Application.Shared.Users.Dtos;
using TDV.DataAccess.Abstract;
using TDV.Entity.Entities.Users;

namespace TDV.Application.Shared.Users
{
    public interface IUserService : IRepository<User>
    {
        Task<FullNameDto> GetUserFullName(int id);
    }
}
