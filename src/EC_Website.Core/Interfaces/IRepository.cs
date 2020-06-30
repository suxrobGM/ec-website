using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EC_Website.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetListAsync();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, string includeString = null, bool disableTracking = true);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
