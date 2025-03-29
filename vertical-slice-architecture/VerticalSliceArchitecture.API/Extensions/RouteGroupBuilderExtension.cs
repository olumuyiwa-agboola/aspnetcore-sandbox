using VerticalSliceArchitecture.API.Filters;
using VerticalSliceArchitecture.API.Endpoints;

namespace VerticalSliceArchitecture.API.Extensions
{
    internal static class RouteGroupBuilderExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        internal static RouteGroupBuilder MapCrudApiEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/Login", Login.HandleRequest)
                .AddEndpointFilter(new FluentValidationFilter());


            return group;
        }
    }
}