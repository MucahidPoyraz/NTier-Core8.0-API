using Common.Enums;

namespace BL.ResponseModels
{
    public interface IResponse
    {
        string Message { get; set; }
        ResponseType ResponseType { get; set; }
    }
}
