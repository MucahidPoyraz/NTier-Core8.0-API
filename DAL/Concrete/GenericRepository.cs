using Common.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entity;
using Common.Models;
using System.Linq; // PaginationParams burada olsun

namespace DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApiContext context)
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

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entity does not support soft delete.");
            }
        }

        public async Task HardDeleteAsync(T entity)
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

        public async Task<List<T>> GetPagedAsync(PaginationParams pagination, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            return await query
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // RowVersion uyuşmazlığı (başka biri aynı kaydı güncellemiş)
                throw;
            }
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync(
    int pageIndex,
    int pageSize,
    Expression<Func<T, bool>> predicate = null,
    Expression<Func<T, object>> orderBy = null,
    bool ascending = true,
    params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            // Filtre uygulanıyor
            if (predicate != null)
                query = query.Where(predicate);

            // Include (ilişkili veriler)
            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

            // Sıralama
            if (orderBy != null)
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            // Toplam kayıt sayısı
            var totalCount = await query.CountAsync();

            // Sayfalama
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // PaginatedList oluşturuluyor
            return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
        }

    }
}
