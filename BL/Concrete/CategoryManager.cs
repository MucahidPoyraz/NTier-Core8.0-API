using BL.Abstract;
using Common.Interfaces;
using Entity;

namespace BL.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryManager
    {
        public CategoryManager(IGenericRepository<Category> repository, ILoggerManager logger) : base(repository, logger)
        {
        }
    }
}
