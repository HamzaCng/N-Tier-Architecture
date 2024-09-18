using Microsoft.EntityFrameworkCore;
using TDV.Entity.Entities.Users;

namespace TDV.DataAccess.Context
{
    public class CvDbContext : DbContext
    {
        public CvDbContext(DbContextOptions options) : base(options) { }      

        public DbSet<User> Users {  get; set; } 
    }
}
