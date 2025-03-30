using FluentValidation;
using Microsoft.Extensions.Options;

namespace VerticalSliceArchitecture.API.Extensions
{
    internal static class OptionsBuilderExtensions
    {
        internal static OptionsBuilder<TOptions> ValidateOptionsWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
        {
            builder.Services.AddSingleton<IValidateOptions<TOptions>>(
                serviceProvider => new OptionsFluentValidationHandler<TOptions>(
                    serviceProvider,
                    builder.Name));

            return builder;
        }
    }

    internal class OptionsFluentValidationHandler<TOptions>(IServiceProvider serviceProvider, string? name) : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly string? _name = name;

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            if (_name != null && _name != name)
            {
                return ValidateOptionsResult.Skip;
            }

            ArgumentNullException.ThrowIfNull(options, nameof(options));

            using var scope = serviceProvider.CreateScope();

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