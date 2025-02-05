using Common.Enums;
using System.Linq.Expressions;

namespace DAL.Abstract
{
    public interface IGenericRepo<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllAsync(OrderType orderType, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(OrderType orderType, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByGuidAsync(object id);
        Task<T> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
