using Common.Enums;

namespace Common.ResponseModels
{
    public interface IResponse
    {
        string Message { get; set; }
        ResponseType ResponseType { get; set; }
    }
}
