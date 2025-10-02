using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Shared.ErrorModels;

namespace DonationSystem.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _Logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _Logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"Endpoint {context.Request.Path} is not found" 
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex,ex.Message);

                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
