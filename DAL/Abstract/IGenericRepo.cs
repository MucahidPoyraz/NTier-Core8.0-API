using System.Linq.Expressions;

namespace DAL.Abstract
{
    public interface IGenericRepo<T>
    {
        // Ekleme
        Task<T> AddAsync(T entity);

        // Güncelleme
        Task UpdateAsync(T entity);

        // Silme
        Task DeleteAsync(T entity);

        // Listeleme
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        // Tekil Kayıt Getirme
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        // Varlık Kontrolü
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
