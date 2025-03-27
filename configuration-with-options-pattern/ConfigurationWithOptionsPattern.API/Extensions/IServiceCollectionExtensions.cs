using FluentValidation;
using ConfigurationWithOptionsPattern.API.Validations;

namespace ConfigurationWithOptionsPattern.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddOpenApi();

            services.AddOptionsWithFluentValidation<AccountInquiryApiSettings, AccountInquiryApiSettingsValidator>(
                AccountInquiryApiSettings.ConfigurationSection);

            return services;
        }

        private static IServiceCollection AddOptionsWithFluentValidation<TOptions, 
            TOptionsValidator>(this IServiceCollection services, string configurationSection) 
            where TOptions : class, new() where TOptionsValidator : AbstractValidator<TOptions>
        {
            services.AddScoped<IValidator<TOptions>, TOptionsValidator>();

            services.AddOptions<TOptions>()
                .BindConfiguration(configurationSection)
                .ValidateOptionsWithFluentValidation()
                .ValidateOnStart();

            return services;
        }
    }
}
