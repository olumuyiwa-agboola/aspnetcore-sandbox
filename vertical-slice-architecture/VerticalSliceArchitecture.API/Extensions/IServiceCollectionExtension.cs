using FluentValidation;
using VerticalSliceArchitecture.API.Models;
using VerticalSliceArchitecture.API.Factories;
using VerticalSliceArchitecture.API.Validations;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.API.Repositories;

namespace VerticalSliceArchitecture.API.Extensions
{
    /// <summary>
    /// Contains methods which adds the required services to the application's service container.
    /// </summary>
    internal static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds and configures OpenAPI services to the application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new()
                    {
                        Version = "v1",
                        Title = "Staff Rating API",
                        Description = "A simple API to manage staff ratings."
                    };

                    return Task.CompletedTask;
                });
            });

            return services;
        }

        /// <summary>
        /// Adds the database connection mechanism and data access classes to the application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            services.AddScoped<IStaffsRepository, StaffsRepository>();

            return services;
        }

        /// <summary>
        /// Adds the FluentValidation classes to the application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            return services;
        }

        /// <summary>
        /// Adds options and the mechanism for their validation on start to the application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddApplicationOptions(this IServiceCollection services)
        {
            services.AddOptionsWithFluentValidation<ConnectionStrings, ConnectionStringsValidator>(ConnectionStrings.ConfigSection);

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