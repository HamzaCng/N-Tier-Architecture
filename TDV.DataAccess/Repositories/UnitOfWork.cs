using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;
using TDV.Entity.Entities.Authentications;
using TDV.Entity.Entities.Users;

namespace TDV.DataAccess.Repositories
{
    public class UnitOfWork(TestDbContext context) : IUnitOfWork
    {
        private IRepository<User> _users;
        private IRepository<RefreshToken> _refreshTokens;



        public IRepository<User> Users
        {
            get { return _users ??= new GenericRepository<User>(context); }
            set { _users = value; }
        }

        public IRepository<RefreshToken> RefreshTokens
        {
            get { return _refreshTokens ??= new GenericRepository<RefreshToken>(context); }
            set { _refreshTokens = value; }
        }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
