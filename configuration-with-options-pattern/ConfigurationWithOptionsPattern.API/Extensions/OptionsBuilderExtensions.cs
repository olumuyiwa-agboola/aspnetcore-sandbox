using ConfigurationWithOptionsPattern.API.Handlers;
using Microsoft.Extensions.Options;

namespace ConfigurationWithOptionsPattern.API.Extensions
{
    public static class OptionsBuilderExtensions
    {
        public static OptionsBuilder<TOptions> ValidateOptionsWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
        {
            builder.Services.AddSingleton<IValidateOptions<TOptions>>(
                serviceProvider => new OptionsFluentValidationHandler<TOptions>(
                    serviceProvider, 
                    builder.Name));

            return builder;
        }
    }
}
