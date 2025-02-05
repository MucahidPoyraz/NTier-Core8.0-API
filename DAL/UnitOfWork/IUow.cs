using DAL.Abstract;

namespace DAL.UnitOfWork
{
    public interface IUow : IAsyncDisposable
    {
        IGenericRepo<T> GetGenericRepo<T>() where T : class, IEquatable<T>;
        Task<int> SaveAsync();
        int Save();
    }
}
