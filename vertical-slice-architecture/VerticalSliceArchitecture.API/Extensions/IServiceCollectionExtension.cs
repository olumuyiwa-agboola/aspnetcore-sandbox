namespace VerticalSliceArchitecture.API.Extensions
{
    /// <summary>
    /// Contains the <see cref="ConfigureApplicationServices"/> method which 
    /// adds the required services to the application's service container.
    /// </summary>
    internal static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds the required services to the application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}