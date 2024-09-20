using Microsoft.EntityFrameworkCore;
using TDV.Entity.Entities.Authentications;
using TDV.Entity.Entities.Users;

namespace TDV.DataAccess.Context
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options) { }      

        public DbSet<User> Users {  get; set; }
        public DbSet<RefreshToken> RefreshTokens{ get; set; }

    }
}
