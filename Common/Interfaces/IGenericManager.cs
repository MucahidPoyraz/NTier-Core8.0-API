using Common.ResponseModels;
using System.Linq.Expressions;

namespace Common.Models
{
    public interface IGenericManager<T>
    {
        Task<ITResponse<T>> AddAsync(T entity);
        Task<IResponse> UpdateAsync(T entity);
        Task<IResponse> DeleteAsync(T entity);
        Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate);

        // Yeni eklenenler
        Task<ITResponse<int>> CountAsync(Expression<Func<T, bool>> predicate = null);

        // Eğer pagination kullanacaksan bu da eklenebilir
        Task<ITResponse<PaginatedList<T>>> GetPaginatedAsync(int pageIndex,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includes);
    }
}
