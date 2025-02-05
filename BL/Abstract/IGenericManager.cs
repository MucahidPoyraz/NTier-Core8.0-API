using BL.ResponseModels;
using System.Linq.Expressions;

namespace BL.Abstract
{
    public interface IGenericManager<T>
    {
        Task<IResponse> AddAsync(T entity);
        Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<ITResponse<T>> GetByGuidAsync(object id);
        Task<ITResponse<T>> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties);
        Task<IResponse> UpdateAsync(T entity);
        Task<IResponse> DeleteAsync(T entity);
        Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
