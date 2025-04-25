using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TransactionsService.Core.Models.Entities;
using TransactionsService.Data.DatabaseContexts;
using TransactionsService.Core.Models.DTOs.ApiRequests;

namespace TransactionsService.API.Endpoints
{

    public class GetTransactionDetails
    {
        internal async static Task<IResult> HandleRequest([FromRoute, 
            Description("The thirty-digit unique identifier for the transaction"),
            RegularExpression(@"\d+"), MinLength(30), MaxLength(30)] string reference, 
            [FromServices] TransactionsDbContext _transactionsDbContext)
        {
            var requestModelState = await new TransactionDetailsRequestValidator().ValidateAsync(new TransactionDetailsRequest(reference));
            
            if (!requestModelState.IsValid)
            {
                ProblemDetails validationFailureResult = new()
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = $"One or more validations failed."
                };

                validationFailureResult.Extensions.Add("Errors", requestModelState.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList());

                return TypedResults.BadRequest(validationFailureResult);
            }

            Transaction? transaction = await _transactionsDbContext.Transactions
                .FirstOrDefaultAsync(t => t.Reference == reference);

            if (transaction is null)
            {
                return TypedResults.NotFound(new ProblemDetails
                {
                    Title = "Transaction Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"Transaction with reference {reference} was not found."
                });
            }

            return TypedResults.Ok(transaction);
        }
    }
}
