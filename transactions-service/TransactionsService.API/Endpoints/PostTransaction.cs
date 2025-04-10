using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TransactionsService.Core.Models.DTOs;
using TransactionsService.Core.Models.Enums;
using TransactionsService.Core.Models.Entities;
using TransactionsService.Data.DatabaseContexts;
using TransactionsService.Core.Features.Validations;

namespace TransactionsService.API.Endpoints
{
    public class PostTransaction
    {
        internal async static Task<IResult> HandleRequest([FromBody] PostTransactionRequest postTransactionRequest, [FromServices] TransactionsDbContext _transactionsDbContext)
        {
            var modelState = await new PostTransactionRequestValidator().ValidateAsync(postTransactionRequest);

            if (!modelState.IsValid)
            {
                ProblemDetails validationFailureResult = new()
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = $"One or more validations failed."
                };

                validationFailureResult.Extensions.Add("Errors", modelState.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList());

                return TypedResults.BadRequest(validationFailureResult);
            }

            var now = DateTime.UtcNow;

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
            {
                return TypedResults.Problem(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An error occurred while posting the transaction."
                });
            }

            PostTransactionResponse postTransactionResponse = new(transaction.Reference, transaction.Status);

            return TypedResults.Ok(postTransactionResponse);
        }
    }
}
