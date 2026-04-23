using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using AppValidationException = Movies.Application.Exceptions.ValidationException;

namespace Movies.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider) 
        { 
            _serviceProvider = serviceProvider; 
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments)
            {

                if (arg.Value == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(arg.Value.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator == null) continue;

                var validationContext = new ValidationContext<object>(arg.Value);
                var result = await validator.ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            g => char.ToLowerInvariant(g.Key[0]) + g.Key.Substring(1),
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        );

                    throw new AppValidationException(errors);
                }


            }

            await next();
        }
    }
}
