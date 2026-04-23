

using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Movies.Application.Validators.Profile;
using Movies.Application.DTOs.Profile;

namespace Movies.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            // FluentValidation
            services.AddValidatorsFromAssemblyContaining<CreateProfileDtoValidator>();
            return services;
        }
    }
}
