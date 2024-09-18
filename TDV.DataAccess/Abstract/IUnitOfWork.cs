using TDV.Entity.Entities.Users;

namespace TDV.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; set; } 

        Task<int> CompleteAsync();
    }
}
