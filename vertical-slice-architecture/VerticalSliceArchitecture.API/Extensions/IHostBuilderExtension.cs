using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace VerticalSliceArchitecture.API.Extensions
{
    /// <summary>
    /// Contains the <see cref="ConfigureSerilogLogger"/>
    /// method which reads the Serilog logger configuration 
    /// from the configuration and adds Serilog to the application's
    /// service container with the logger configuration.
    /// </summary>
    internal static class IHostBuilderExtension
    {
        /// <summary>
        /// Creates a Serilog logger configuration and adds Serilog 
        /// to the application's service container with the logger configuration.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        internal static IHostBuilder ConfigureSerilogLogger(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, loggerConfig) => loggerConfig
                                            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                                            .ReadFrom.Configuration(context.Configuration)
                                            .Enrich.FromLogContext());

            return hostBuilder;
        }
    }
}
