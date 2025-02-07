using BL.ResponseModels;
using System.Linq.Expressions;

namespace BL.Abstract
{
    public interface IGenericManager<T>
    {
        Task<ITResponse<T>> AddAsync(T entity);
        Task<IResponse> UpdateAsync(T entity);
        Task<IResponse> DeleteAsync(T entity);
        Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);     
        Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
