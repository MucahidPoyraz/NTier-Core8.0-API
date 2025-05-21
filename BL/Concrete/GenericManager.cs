using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Common.ResponseModels;
using System.Linq.Expressions;

namespace BL.Concrete
{
    public class GenericManager<T> : IGenericManager<T> where T : class, new()
    {
        private readonly IGenericRepository<T> _repository;
        private readonly ILoggerManager _logger;

        public GenericManager(IGenericRepository<T> repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        private TResponse<TRes> HandleException<TRes>(Exception ex)
        {
            string message = $"[GenericManager<{typeof(T).Name}>] An error occurred: {ex.Message}";
            _logger.LogError(message);

            return new TResponse<TRes>
            {
                Message = "An unexpected error occurred. Please contact support.",
                ResponseType = ResponseType.Error
            };
        }
        public async Task<ITResponse<T>> AddAsync(T entity)
        {
            try
            {
                var addedEntity = await _repository.AddAsync(entity);
                _logger.LogInfo($"{typeof(T).Name} entity added successfully.");
                return new TResponse<T>
                {
                    Message = "Entity added successfully.",
                    ResponseType = ResponseType.Success,
                    Data = addedEntity
                };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<IResponse> UpdateAsync(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                _logger.LogInfo($"{typeof(T).Name} entity updated successfully.");
                return new TResponse<T>
                {
                    Message = "Entity updated successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<IResponse> DeleteAsync(T entity)
        {
            try
            {
                await _repository.DeleteAsync(entity);
                _logger.LogInfo($"{typeof(T).Name} entity deleted successfully.");
                return new TResponse<T>
                {
                    Message = "Entity deleted successfully.",
                    ResponseType = ResponseType.Success
                };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ITResponse<List<T>>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var data = await _repository.GetAllAsync(predicate, includes);
                _logger.LogInfo($"{typeof(T).Name} entities retrieved successfully.");
                return new TResponse<List<T>>
                {
                    Message = "Entities retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return HandleException<List<T>>(ex);
            }
        }

        public async Task<ITResponse<T>> GetAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var data = await _repository.GetAsync(predicate, includes);
                if (data == null)
                {
                    _logger.LogWarning($"{typeof(T).Name} entity not found.");
                    return new TResponse<T>
                    {
                        Message = "Entity not found.",
                        ResponseType = ResponseType.NotFound
                    };
                }

                _logger.LogInfo($"{typeof(T).Name} entity retrieved successfully.");
                return new TResponse<T>
                {
                    Message = "Entity retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var exists = await _repository.AnyAsync(predicate);
                _logger.LogInfo($"Existence check for {typeof(T).Name} completed. Result: {exists}");
                return new TResponse<bool>
                {
                    Message = "Existence check completed.",
                    ResponseType = ResponseType.Success,
                    Data = exists
                };
            }
            catch (Exception ex)
            {
                return HandleException<bool>(ex);
            }
        }

        public async Task<ITResponse<int>> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                var count = await _repository.CountAsync(predicate);
                _logger.LogInfo($"{typeof(T).Name} count retrieved successfully. Count: {count}");
                return new TResponse<int>
                {
                    Message = "Count retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                return HandleException<int>(ex);
            }
        }

        public async Task<ITResponse<PaginatedList<T>>> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var pagedData = await _repository.GetPaginatedAsync(pageIndex, pageSize, predicate, orderBy, ascending, includes);
                _logger.LogInfo($"{typeof(T).Name} paginated data retrieved successfully.");
                return new TResponse<PaginatedList<T>>
                {
                    Message = "Paginated data retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = pagedData
                };
            }
            catch (Exception ex)
            {
                return HandleException<PaginatedList<T>>(ex);
            }
        }

        public Task<ITResponse<PaginatedList<T>>> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
