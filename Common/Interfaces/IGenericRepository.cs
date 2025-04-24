using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IGenericRepository<T>
    {
        // Ekleme
        Task<T> AddAsync(T entity);

        // Güncelleme (row version için overload eklenebilir)
        Task UpdateAsync(T entity);

        // Silme (Soft Delete varsayılan olarak yapılacak)
        Task DeleteAsync(T entity);
        Task HardDeleteAsync(T entity);

        // Tümünü listeleme (filtre opsiyonel, include opsiyonel)
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<PaginatedList<T>> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties);
        // Tekil kayıt getirme
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        // Varlık var mı kontrolü
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Count (soft delete filtreli)
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
