using Common.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUow : IAsyncDisposable
    {
        IGenericRepository<T> GetGenericRepo<T>() where T : class, IEquatable<T>;
        Task<int> SaveAsync();
        int Save();
    }
}
