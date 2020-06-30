using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Interfaces;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<TEntity> GetByIdAsync(string id)
        {
            return _context.Set<TEntity>().SingleOrDefaultAsync(i => i.Id == id);
        }

        public Task<List<TEntity>> GetListAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (disableTracking)
            {
                query = _context.Set<TEntity>().AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            return query.Where(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(TEntity entity)
        {
            var sourceEntity = _context.Set<TEntity>().FirstOrDefault(i => i.Id == entity.Id);

            if (sourceEntity == null) 
                return Task.CompletedTask;

            _context.Remove(sourceEntity);
            return _context.SaveChangesAsync();
        }
    }
}
