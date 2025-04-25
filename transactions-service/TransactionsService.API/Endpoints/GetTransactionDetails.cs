using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionsService.Core.Factories;
using System.ComponentModel.DataAnnotations;
using TransactionsService.Core.Models.Entities;
using TransactionsService.Data.DatabaseContexts;
using TransactionsService.Core.Models.DTOs.ApiRequests;
using TransactionsService.Core.Models.DTOs.ApiResponses;

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
                return TypedResults.BadRequest(ProblemDetailsFactory.CreateBadRequestResponseFromFluentValidationResult(requestModelState.Errors));

            Transaction? transaction = await _transactionsDbContext.Transactions
                .FirstOrDefaultAsync(t => t.Reference == reference);

            if (transaction is null)
                return TypedResults.BadRequest(ProblemDetailsFactory.CreateNotFoundResponse("Transaction", "reference", reference));

            return TypedResults.Ok(new GetTransactionDetailsResponse(transaction));
        }
    }
}
