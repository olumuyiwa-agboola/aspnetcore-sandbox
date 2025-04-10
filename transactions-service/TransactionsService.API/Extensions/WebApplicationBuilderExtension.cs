using Serilog;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Serilog.Sinks.SystemConsole.Themes;
using TransactionsService.Data.DatabaseContexts;
using TransactionsService.Core.Features.Validations;
using TransactionsService.Core.Utilities.Configuration;

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
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

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

            var transactionsDbConnectionString = builder.Configuration
                .GetSection(ConnectionStrings.ConfigSection)
                .Get<ConnectionStrings>()!
                .TransactionsDbConnectionString;

            builder.Services.AddDbContext<TransactionsDbContext>(options => 
                options.UseMySQL(transactionsDbConnectionString!));

            builder.Services.AddValidatorsFromAssemblyContaining<TransactionDetailsRequestValidator>();

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