
﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Application.Exceptions;
using Movies.Infrastructure.Extensions;

namespace Movies.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex.Message);
                var problemDetails = ex.ToProblemDetails(httpContext.Request.Path);
                httpContext.Response.StatusCode = problemDetails.Status ?? 500;
                httpContext.Response.ContentType = "application/problem+json";
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (BadHttpRequestException ex)
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/problem+json";
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "Invalid request format",
                    Status = 400,
                    Type = "https://httpstatuses.com/400",
                    Instance = httpContext.Request.Path
                });
            }
            catch (System.Text.Json.JsonException ex)
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/problem+json";
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "Invalid JSON format",
                    Status = 400,
                    Type = "https://httpstatuses.com/400",
                    Instance = httpContext.Request.Path
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/problem+json";
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred",
                    Status = 500,
                    Type = "https://httpstatuses.com/500",
                    Instance = httpContext.Request.Path
                });
            }
        }
    }
}
