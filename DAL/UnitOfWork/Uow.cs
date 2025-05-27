using Common.Interfaces;
using DAL.Concrete;
using DAL.Context;

namespace DAL.UnitOfWork
{
    public class Uow : IUOW
    {
        private readonly ApiContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public Uow(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<T> GetGenericRepo<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
