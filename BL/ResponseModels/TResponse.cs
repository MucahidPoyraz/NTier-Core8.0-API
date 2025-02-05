using Common.Enums;

namespace BL.ResponseModels
{
    public class TResponse<T> : Response, ITResponse<T>
    {
        public T Data { get; set; }
        public List<CustomValidatonError> ValidationError { get; set; }

        // Varsayılan yapıcı
        public TResponse() { }

        public TResponse(ResponseType responseType, string message) : base(responseType, message)
        {
        }

        public TResponse(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }

        public TResponse(T data, List<CustomValidatonError> errors) : base(ResponseType.ValidationError)
        {
            ValidationError = errors;
            Data = data;
        }
    }
}
