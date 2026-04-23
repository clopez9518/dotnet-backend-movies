using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common;

namespace Movies.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => char.ToLowerInvariant(kvp.Key[0]) + kvp.Key.Substring(1),
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var response = new ErrorResponse
                    {
                        Message = "Invalid request",
                        Code = "VALIDATION_ERROR",
                        StatusCode = 400,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
