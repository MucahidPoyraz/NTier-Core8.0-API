using Common.Enums;

namespace BL.ResponseModels
{
    public class Response : IResponse
    {
        public Response()
        {
                
        }
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }

        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }

        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
    }
}
