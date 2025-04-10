using Microsoft.AspNetCore.Mvc;
using TransactionsService.API.Filters;
using TransactionsService.API.Endpoints;
using TransactionsService.Core.Models.Entities;

namespace TransactionsService.API.Extensions
{
    internal static class RouteGroupBuilderExtension
    {
        internal static RouteGroupBuilder MapTransactionEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/{reference}", GetTransactionDetails.HandleRequest)
                .WithSummary("Get Transaction Details")
                .WithDescription("Retrieves the details of a transaction using the transaction reference.")
                .Produces<Transaction>(200)
                .Produces<ProblemDetails>(400)
                .Produces<ProblemDetails>(404)
                .Produces<ProblemDetails>(500)
                .AddEndpointFilter(new FluentValidationFilter());

            group.MapPost("/", PostTransaction.HandleRequest)
                .WithSummary("Post Transaction")
                .WithDescription("Posts a new transaction.")
                .Produces<Transaction>(200)
                .Produces<ProblemDetails>(400)
                .Produces<ProblemDetails>(500)
                .AddEndpointFilter(new FluentValidationFilter());

            return group;
        }
    }
}