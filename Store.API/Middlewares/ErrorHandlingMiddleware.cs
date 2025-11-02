
using Store.Domain.Exceptions;

namespace Store.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotValidDtoException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);

                _logger.LogWarning(ex.Message);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);

                _logger.LogWarning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Internal Server Error. {ex.Message}");
            }
        }
    }
}
