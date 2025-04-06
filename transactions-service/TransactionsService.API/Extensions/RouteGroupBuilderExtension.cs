using TransactionsService.API.Filters;
using TransactionsService.Core.Models;
using TransactionsService.API.Endpoints;

namespace TransactionsService.API.Extensions
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