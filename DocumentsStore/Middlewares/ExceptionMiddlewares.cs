using DocumentsStore.Middlewares;
using DocumentStore.BL.Exceptions;
using System.Collections;
using System.Net;
using System.Text;

namespace DocumentsStore.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown: {ex}. Data: {LogExceptionData(ex.Data)}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private string LogExceptionData(IDictionary data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in data.Keys)
            {
                sb.Append($"{item}:{data[item]}");
            }
            return sb.ToString();
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is DocumentNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is FormatNotSupportedException or DocumentAlreadyExistsException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await context.Response.WriteAsJsonAsync(new ErrorDetails(context.Response.StatusCode, exception.Message).ToString());
        }
    }

    public record ErrorDetails(int StatusCode, string Message);
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<ExceptionMiddleware>();
    }
}