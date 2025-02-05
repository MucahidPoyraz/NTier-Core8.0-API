using DAL.Abstract;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, IEquatable<T>
    {
        private readonly ApiContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(ApiContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while checking the existence of the entity.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties != null)
                {
                    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                query = query.Where(predicate);
                if (includeProperties != null)
                {
                    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                }
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while retrieving the entity.", ex);
            }
        }

        public async Task<T> GetByGuidAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while retrieving the entity by ID.", ex);
            }
        }

        public async Task<T> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id").Equals(id));
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while retrieving the entity with included properties.", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata günlüğe kaydedilebilir veya özel bir hata yönetimi yapılabilir.
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
