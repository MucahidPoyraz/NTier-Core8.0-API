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

        private static TResponse<TRes> HandleException<TRes>(Exception ex) =>
            new()
            {
                Message = $"An error occurred: {ex.Message}",
                ResponseType = ResponseType.Error
            };

        public async Task<ITResponse<T>> AddAsync(T entity)
        {
            try
            {
                var addedEntity = await _repository.AddAsync(entity);
                return new TResponse<T> { Message = "Entity added successfully.", ResponseType = ResponseType.Success, Data = addedEntity };
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
                return new TResponse<bool> { Message = "Existence check completed.", ResponseType = ResponseType.Success, Data = await _repository.AnyAsync(predicate) };
            }
            catch (Exception ex)
            {
                return HandleException<bool>(ex);
            }
        }

        public async Task<IResponse> DeleteAsync(T entity)
        {
            try
            {
                await _repository.DeleteAsync(entity);
                return new TResponse<T> { Message = "Entity deleted successfully.", ResponseType = ResponseType.Success };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ITResponse<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return new TResponse<List<T>> { Message = "Entities retrieved successfully.", ResponseType = ResponseType.Success, Data = await _repository.GetAllAsync(predicate, includes) };
            }
            catch (Exception ex)
            {
                return HandleException<List<T>>(ex);
            }
        }       

        public async Task<ITResponse<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return new TResponse<T> { Message = "Entity retrieved successfully.", ResponseType = ResponseType.Success, Data = await _repository.GetAsync(predicate, includes) };
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
                return new TResponse<T> { Message = "Entity updated successfully.", ResponseType = ResponseType.Success, Data = entity };
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }
    }
}
