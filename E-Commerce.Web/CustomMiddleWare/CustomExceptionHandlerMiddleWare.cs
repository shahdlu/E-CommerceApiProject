using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWare
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return object as jason
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
