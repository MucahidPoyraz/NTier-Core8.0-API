using BL.ResponseModels;
using Common.Enums;
using System.Linq.Expressions;

namespace BL.Abstract
{
    public interface IGenericManager<T>
    {
        Task<ITResponse<T>> AddAsync(T entity);
        Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<List<T>>> GetAllAsync(OrderType orderType, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<T>> GetAsync(OrderType orderType, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<T>> GetByGuidAsync(object id);
        Task<ITResponse<T>> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties);
        Task<IResponse> UpdateAsync(T entity);
        Task<IResponse> DeleteAsync(object id);
        Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
