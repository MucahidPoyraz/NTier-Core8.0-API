using Common.ResponseModels;

namespace Common.Interfaces
{
    public interface IUOW : IAsyncDisposable
    {
        IGenericRepository<T> GetGenericRepo<T>() where T : class;
        Task<int> SaveAsync();
        int Save();
    }
}
