using Serilog;
using FluentValidation;
using TransactionsService.Core.Models;
using Serilog.Sinks.SystemConsole.Themes;
using TransactionsService.API.Repositories;
using TransactionsService.Core.Abstractions;
using TransactionsService.Core.Features.Factories;
using TransactionsService.Core.Features.Validations;

namespace TransactionsService.API.Extensions
{
    /// <summary>
    /// Contains methods which adds the required services to the application's service container.
    /// </summary>
    internal static class WebApplicationBuilderExtension
    {
        /// <summary>
        /// Adds and configures the required services to the application's service container.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new()
                    {
                        Version = "v1",
                        Title = "Fake Bank Transactions Service API",
                        Description = """
                        The Fake Bank Transactions Service API provides a secure and scalable interface for managing 
                        customer transactions within the Fake Bank ecosystem. This RESTful API allows authorized clients 
                        to perform operations such as creating new transactions, retrieving transaction history, updating 
                        transaction details, and managing transaction statuses.
                        
                        Designed with reliability and performance in mind, this service forms a core part of Fake Bank's 
                        digital infrastructure, enabling seamless integration with internal systems and third-party services.
                        """
                    };

                    return Task.CompletedTask;
                });
            });

            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            builder.Services.AddScoped<IStaffsRepository, StaffsRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            builder.Services.AddOptionsWithFluentValidation<ConnectionStrings, ConnectionStringsValidator>(ConnectionStrings.ConfigSection);

            builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
                                            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                                            .ReadFrom.Configuration(context.Configuration)
                                            .Enrich.FromLogContext());

            return builder.Build();
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