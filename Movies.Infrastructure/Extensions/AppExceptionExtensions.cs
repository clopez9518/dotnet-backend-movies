
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Exceptions;

namespace Movies.Infrastructure.Extensions
{
    public static class AppExceptionExtensions
    {
        public static ProblemDetails ToProblemDetails(this AppException ex, string? instance = null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ex.GetType().Name.Replace("Exception", ""),
                Detail = ex.Message,
                Status = ex.StatusCode,
                Type = $"https://httpstatuses.com/{ex.StatusCode}",
                Instance = instance,
            };

            if (ex is ValidationException validationException)
            {
                problemDetails.Extensions["errors"] = validationException.Errors;
            }

            if (ex is AppException appEx)
            {
                problemDetails.Extensions["code"] = appEx.Code;
            }

            return problemDetails;
        }
    }
}