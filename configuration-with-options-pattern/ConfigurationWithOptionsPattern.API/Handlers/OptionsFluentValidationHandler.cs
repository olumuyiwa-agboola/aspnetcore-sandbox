using FluentValidation;
using Microsoft.Extensions.Options;

namespace ConfigurationWithOptionsPattern.API.Handlers
{
    public class OptionsFluentValidationHandler<TOptions> : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly string? _name;
        private readonly IServiceProvider _serviceProvider;

        public OptionsFluentValidationHandler(IServiceProvider serviceProvider, string? name)
        {
            _name = name;
            _serviceProvider = serviceProvider;
        }

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            if (_name != null && _name != name)
            {
                return ValidateOptionsResult.Skip;
            }

            ArgumentNullException.ThrowIfNull(options, nameof(options));

            using var scope = _serviceProvider.CreateScope();

            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

            var result = validator.Validate(options);

            if (result.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var type = options.GetType().Name;
            var errors = new List<string>();

            foreach (var error in result.Errors)
            {
                errors.Add($"Validation failed for {type}.{error.PropertyName}: with error: {error.ErrorMessage}");
            }

            return ValidateOptionsResult.Fail(errors);
        }
    }
}
