namespace BL.ResponseModels
{
    public interface ITResponse<T> : IResponse
    {
        T Data { get; set; }
        List<CustomValidatonError> ValidationError { get; set; }
    }
}
