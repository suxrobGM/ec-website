using System.Collections.Generic;
using System.Threading.Tasks;

namespace EC_Website.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<List<TEntity>> GetListAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
