using VerticalSliceArchitecture.API.Models;
using VerticalSliceArchitecture.API.Filters;
using VerticalSliceArchitecture.API.Endpoints;

namespace VerticalSliceArchitecture.API.Extensions
{
    internal static class RouteGroupBuilderExtension
    {
        internal static RouteGroupBuilder MapStaffsEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/Login", Login.HandleRequest)
                .ValidateDataAnnotations<LoginRequest>()
                .AddEndpointFilter(new FluentValidationFilter());

            return group;
        }
    }
}