using Common.Enums;

namespace Common.ResponseModels
{
    public class SuccessResponse<T> : BaseResponse<T>
    {
        public SuccessResponse(T data, string message = "")
        {
            Data = data;
            Message = message;
            ResponseType = ResponseType.Success;
        }
    }
}
