using System.Linq.Expressions;
using TDV.Core.Entities;

namespace TDV.DataAccess.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region Write Repo
        Task<bool> AddAsync(T entity);
        Task<bool> RemoveAsync(int id);
        Task<bool> Update(T entity);
        #endregion

        #region Read Repo
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        Task<int> Count();
        Task<int> FilteredCount(Expression<Func<T, bool>> predicate);    
        #endregion
    }
}
