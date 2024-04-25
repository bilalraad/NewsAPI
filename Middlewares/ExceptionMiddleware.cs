using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NewsAPI.Errors;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Create the ApiError object
                var apiError = new ApiError
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while processing your request."
                };

                // Customize the error response based on the environment
                if (_environment.IsDevelopment())
                {
                    apiError.Details = ex.StackTrace.ToString();
                }
                else
                {
                    apiError.Details = "Please contact support for more information.";
                }

                // Set the response status code and content type
                context.Response.StatusCode = apiError.StatusCode;
                context.Response.ContentType = "application/json";

                // Serialize the ApiError object to JSON
                var json = JsonSerializer.Serialize(apiError);

                // Write the JSON response to the client
                await context.Response.WriteAsync(json);
            }
        }
    }


}