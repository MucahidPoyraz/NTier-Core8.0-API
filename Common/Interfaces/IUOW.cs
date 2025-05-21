namespace Common.Interfaces
{
    public interface IUOW : IAsyncDisposable
    {
        IGenericRepository<T> GetGenericRepo<T>() where T : class, IEquatable<T>;
        Task<int> SaveAsync();
        int Save();
    }
}
