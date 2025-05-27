using Common.Enums;

namespace Common.ResponseModels
{
    public class ErrorResponse<T> : BaseResponse<T>
    {
        public List<string>? Errors { get; init; }

        public ErrorResponse(string message, ResponseType responseType = ResponseType.Error)
        {
            Data = default;
            Message = message;
            ResponseType = responseType;
        }

        public ErrorResponse(List<string> errors, ResponseType responseType = ResponseType.ValidationError)
        {
            Data = default;
            Errors = errors;
            Message = string.Join(" | ", errors);
            ResponseType = responseType;
        }
    }
}
