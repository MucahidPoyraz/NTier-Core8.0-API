﻿using System.Linq.Expressions;

namespace DAL.Abstract
{
    public interface IGenericRepo<T>
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

        // Tekil kayıt getirme
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        // Varlık var mı kontrolü
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Count (soft delete filtreli)
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
