using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UpsMo.Common.Response;

namespace UpsMo.WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;

                var responseBody = JsonSerializer.Serialize(new ApiResponse(ResponseStatus.Internal, ResponseMessage.Internal));

                _logger.LogError(error, responseBody);

                await response.WriteAsync(responseBody);
            }
        }
    }
}