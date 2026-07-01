using Microsoft.AspNetCore.Diagnostics;
using test_bp.Shared.Messaging;
using System.Net;

namespace Nexo.Api.Infrastructure.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Ha ocurrido un error no controlado: {Message}", exception.Message);

            var response = ApiResponse<object>.InternalError("Lo sentimos, ha ocurrido un error interno en el servidor.");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
