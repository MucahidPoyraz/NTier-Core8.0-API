using DAL.Abstract;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApiContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(ApiContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> orderBy, bool ascending = true, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
