using System.Net;
using System.Text.Json;
using Core.Constants;
using Core.Constants.CustomExceptions;

namespace Web.Middlewares
{
    public class GlobalErrorsHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorsHandler> _logger;

        public GlobalErrorsHandler(RequestDelegate next, ILogger<GlobalErrorsHandler> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var loggingLevel = LogLevel.Error;

                switch (exception)
                {
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        loggingLevel = LogLevel.Information;
                        break;

                    case BadRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        loggingLevel = LogLevel.Information;
                        break;

                    case UnauthorizedException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        loggingLevel = LogLevel.Information;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var exceptionResponse = new
                {
                    exceptionType = exception.GetType().Name,
                    statusCode = response.StatusCode,
                    message = exception.Message,
                };

                _logger.Log(logLevel: loggingLevel, LogEvents.ExceptionForm, exceptionResponse.exceptionType, exceptionResponse.statusCode, exceptionResponse.message);

                var result = JsonSerializer.Serialize(exceptionResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
