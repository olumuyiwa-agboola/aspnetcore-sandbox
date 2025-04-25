using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TransactionsService.Core.Factories;
using TransactionsService.Core.Models.Enums;
using TransactionsService.Core.Models.Entities;
using TransactionsService.Data.DatabaseContexts;
using TransactionsService.Core.Models.DTOs.ApiRequests;
using TransactionsService.Core.Models.DTOs.ApiResponses;

namespace TransactionsService.API.Endpoints
{
    public class PostTransaction
    {
        internal async static Task<IResult> HandleRequest([FromBody] PostTransactionRequest postTransactionRequest, [FromServices] TransactionsDbContext _transactionsDbContext)
        {
            var modelState = await new PostTransactionRequestValidator().ValidateAsync(postTransactionRequest);

            if (!modelState.IsValid)
                return TypedResults.BadRequest(ProblemDetailsFactory.CreateBadRequestResponseFromFluentValidationResult(modelState.Errors));

            var now = DateTime.Now;

            Transaction transaction = new()
            {
                DateTime = now,
                Status = TransactionStatus.Pending,
                Amount = postTransactionRequest.Amount,
                Remarks = postTransactionRequest.Remarks,
                Currency = postTransactionRequest.Currency,
                SenderName = postTransactionRequest.SenderName,
                SenderBankName = postTransactionRequest.SenderBankName,
                BeneficiaryName = postTransactionRequest.BeneficiaryName,
                SenderAccountNumber = postTransactionRequest.SenderAccountNumber,
                BeneficiaryBankName = postTransactionRequest.BeneficiaryBankName,
                BeneficiaryAccountNumber = postTransactionRequest.BeneficiaryAccountNumber,
                Reference = postTransactionRequest.SenderAccountNumber.Substring(0, 5)
                            + postTransactionRequest.BeneficiaryAccountNumber.Substring(5, 5) 
                            + $"{now:yyyyMMddHHmmss}" + $"{RandomNumberGenerator.GetInt32(100_000, 1_000_000)}",
            };

            _transactionsDbContext.Add(transaction);
            int dbResult = _transactionsDbContext.SaveChanges();

            if (dbResult != 1)
                return TypedResults.Problem(ProblemDetailsFactory.CreateInternalServerErrorResponse("An error occurred while posting the transaction"));

            PostTransactionResponse postTransactionResponse = new(transaction.Reference, transaction.Status);

            return TypedResults.Ok(postTransactionResponse);
        }
    }
}
