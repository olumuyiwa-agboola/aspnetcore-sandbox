using FluentValidation;

namespace ConfigurationWithOptionsPattern.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOptionsWithFluentValidation<TOptions, TOptionsValidator>(this IServiceCollection services, string configurationSection) where TOptions : class, new() where TOptionsValidator : AbstractValidator<TOptions>
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
