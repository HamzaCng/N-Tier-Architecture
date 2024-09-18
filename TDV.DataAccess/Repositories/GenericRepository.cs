using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TDV.Core.Entities;
using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;

namespace TDV.DataAccess.Repositories
{
    public class GenericRepository<T>(CvDbContext context) : IRepository<T> where T : BaseEntity
    {
        //Her seferinde set etmemek için.
        public DbSet<T> Table { get => context.Set<T>(); }

        #region Write Repo
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);

            if (entityEntry.State == EntityState.Added)
            {
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            await context.SaveChangesAsync();

            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = true;
            EntityEntry<T> entityEntry = Table.Update(entity);
            await context.SaveChangesAsync();

            return entityEntry.State == EntityState.Modified;
        }
        #endregion

        #region Read Repo
        public async Task<int> Count()
        {
            return await Table.AsNoTracking().CountAsync();
        }

        public async Task<int> FilteredCount(Expression<Func<T, bool>> predicate)
        {
            return await Table.AsNoTracking().Where(predicate).CountAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Table.AsNoTracking().Where(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Table.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.AsNoTracking().Where(x => x.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);
        }

        #endregion
    }
}
