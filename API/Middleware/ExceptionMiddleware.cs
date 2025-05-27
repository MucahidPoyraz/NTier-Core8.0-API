using Common.Enums;
using Common.ResponseModels;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var message = _env.IsDevelopment()
                    ? ex.Message
                    : "Sunucu tarafında beklenmeyen bir hata oluştu.";

                var response = new Response(ResponseType.Error, message);

                var json = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
