using FluentValidation;
using FluentValidation.Results;

namespace VerticalSliceArchitecture.API.Filters
{
    /// <summary>
    /// Enables the validation of parameters passed to a model before the execution of the assiciated endpoint.
    /// </summary>
    public class FluentValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var serviceProvider = context.HttpContext.RequestServices;

            foreach (var argument in context.Arguments)
            {
                if (argument == null)
                {
                    return Results.BadRequest("Request body cannot be null.");
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(argument!.GetType());

                if (serviceProvider.GetService(validatorType) is IValidator validator)
                {
                    ValidationResult validationResult = validator.Validate(new ValidationContext<object>(argument));

                    if (!validationResult.IsValid)
                    {
                        string validationFailures = string.Empty;
                        foreach (ValidationFailure failure in validationResult.Errors)
                        {
                            if (string.IsNullOrWhiteSpace(validationFailures))
                                validationFailures += failure.PropertyName + ": " + failure.ErrorMessage;
                            else
                                validationFailures += " | " + failure.PropertyName + ": " + failure.ErrorMessage;
                        }

                        return Results.BadRequest(validationFailures);
                    }
                }
            }

            return await next(context);
        }
    }
}