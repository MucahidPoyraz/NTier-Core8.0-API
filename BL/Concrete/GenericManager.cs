using BL.Abstract;
using BL.ResponseModels;
using Common.Enums;
using DAL.Abstract;
using System.Linq.Expressions;

namespace BL.Concrete
{
    public class GenericManager<T> : IGenericManager<T> where T : class, new()
    {
        private readonly IGenericRepo<T> _repository;

        public GenericManager(IGenericRepo<T> repository)
        {
            _repository = repository;
        }

        public async Task<ITResponse<T>> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                return new TResponse<T>
                {
                    Message = "Entity added successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<bool>> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                bool exists = await _repository.AnyAsync(predicate);
                return new TResponse<bool>
                {
                    Message = "Existence check completed successfully.",
                    ResponseType = ResponseType.Success,
                    Data = exists
                };
            }
            catch (Exception ex)
            {
                return new TResponse<bool>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<IResponse> DeleteAsync(object id)
        {
            try
            {
                T entity = await _repository.GetByGuidAsync(id);
                await _repository.DeleteAsync(entity);
                return new TResponse<T>
                {
                    Message = "Entity deleted successfully.",
                    ResponseType = ResponseType.Success
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entities = await _repository.GetAllAsync(predicate, includeProperties);
                return new TResponse<List<T>>
                {
                    Message = "Entities retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new TResponse<List<T>>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<List<T>>> GetAllAsync(OrderType orderType, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entities = await _repository.GetAllAsync(orderType, predicate, includeProperties);
                return new TResponse<List<T>>
                {
                    Message = "Entities retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new TResponse<List<T>>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = await _repository.GetAsync(predicate, includeProperties);
                return new TResponse<T>
                {
                    Message = "Entity retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<T>> GetAsync(OrderType orderType, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = await _repository.GetAsync(orderType, predicate, includeProperties);
                return new TResponse<T>
                {
                    Message = "Entity retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<T>> GetByGuidAsync(object id)
        {
            try
            {
                var entity = await _repository.GetByGuidAsync(id);
                return new TResponse<T>
                {
                    Message = "Entity retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<ITResponse<T>> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var entity = await _repository.GetByGuidAsync(id, includeProperties);
                return new TResponse<T>
                {
                    Message = "Entity retrieved successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }

        public async Task<IResponse> UpdateAsync(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                return new TResponse<T>
                {
                    Message = "Entity updated successfully.",
                    ResponseType = ResponseType.Success,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new TResponse<T>
                {
                    Message = $"An error occurred: {ex.Message}",
                    ResponseType = ResponseType.Error,
                    ValidationError = new List<CustomValidatonError>
                    {
                        new CustomValidatonError { ErrorMessage = ex.Message }
                    }
                };
            }
        }
    }
}
