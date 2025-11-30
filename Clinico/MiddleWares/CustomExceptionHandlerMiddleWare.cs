using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace Clinico.MiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
        public CustomExceptionHandlerMiddleWare(RequestDelegate next,
            ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
                // Handle 404 Not Found
                await HandleNotFoundAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorToReturn()
            {
                Message = ex.Message,
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAutherizedExcepion => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors( badRequestException,  response),
                _ => StatusCodes.Status500InternalServerError

            };
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
        

        private static async Task HandleNotFoundAsync(HttpContext context)
        {
            if(context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"End Point {context.Request.Path} Not Found"
                });
            }
        }
    }
}
