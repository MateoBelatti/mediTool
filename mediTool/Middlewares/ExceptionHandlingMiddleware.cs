using System.Net;
using System.Text.Json;
using Utils.Exceptions;

namespace mediTool.Middlewares
{
    public class ApiErrorResponse
    {
        public string Status { get; set; } = "error";
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Details { get; set; } = new();
    }

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocurrió un error inesperado en el servidor.";
            var details = new List<string>();

            switch (exception)
            {
                case InternalError internalEx:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Error interno del servidor.";
                    if (_env.IsDevelopment())
                    {
                        details.Add(internalEx.Message);
                        details.AddRange(internalEx.Details);
                    }
                    break;
                case AppException appEx:
                    statusCode = appEx.StatusCode;
                    message = appEx.Message;
                    details = appEx.Details;
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "El recurso solicitado no existe.";
                    details.Add(exception.Message);
                    break;
                case ArgumentException or InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "La solicitud no es válida.";
                    details.Add(exception.Message);
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "No está autorizado para acceder a este recurso.";
                    details.Add(exception.Message);
                    break;
                default:
                    if (_env.IsDevelopment())
                    {
                        details.Add(exception.Message);
                        if (exception.StackTrace != null)
                        {
                            details.Add(exception.StackTrace);
                        }
                    }
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new ApiErrorResponse
            {
                Status = "error",
                Code = (int)statusCode,
                Message = message,
                Details = details
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}


