using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            result = JsonSerializer.Serialize(new
            {
                code = code,
                error = exception.Message,
                stacktrace = exception.StackTrace,
                helpLink = exception.HelpLink
            });

            logger.LogError(result);
            return context.Response.WriteAsync(result);
        }
    }
}
