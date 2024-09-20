using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;
using TDV.Entity.Entities.Users;

namespace TDV.DataAccess.Repositories
{
    public class UnitOfWork(TestDbContext context) : IUnitOfWork
    {
        private IRepository<User> _users;      


        public IRepository<User> Users
        {
            get { return _users ??= new GenericRepository<User>(context); }
            set { _users = value; }
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
