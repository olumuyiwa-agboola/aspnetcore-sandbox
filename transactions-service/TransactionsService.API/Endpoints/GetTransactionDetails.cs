using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TransactionsService.Core.Models.Entities;
using TransactionsService.Data.DatabaseContexts;

namespace TransactionsService.API.Endpoints
{
    public class GetTransactionDetails
    {
        internal async static Task<IResult> HandleRequest(
            [FromRoute, Description("The thirty-digit unique identifier for the transaction"), 
            RegularExpression(@"\d+"), MinLength(30), MaxLength(30)] string reference, [FromServices] TransactionsDbContext _transactionsDbContext)
        {
            Transaction? transaction = await _transactionsDbContext.Transactions
                .FirstOrDefaultAsync(t => t.Reference == reference);

            if (transaction is null)
            {
                return TypedResults.NotFound(new ProblemDetails
                {
                    Title = "Transaction Not Found",
                    Detail = $"Transaction with reference {reference} was not found.",
                    Status = StatusCodes.Status404NotFound
                });
            }

            return TypedResults.Ok(transaction);
        }
    }
}
