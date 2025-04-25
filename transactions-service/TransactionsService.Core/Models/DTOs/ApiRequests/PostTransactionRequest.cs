using FluentValidation;
using TransactionsService.Core.Models.Enums;

namespace TransactionsService.Core.Models.DTOs.ApiRequests
{
    public record PostTransactionRequest
    (
        string SenderName,

        string SenderBankName,

        string SenderAccountNumber,

        string BeneficiaryName,

        string BeneficiaryBankName,

        string BeneficiaryAccountNumber,

        Currency Currency,

        string Remarks,

        decimal Amount
    );

    public class PostTransactionRequestValidator : AbstractValidator<PostTransactionRequest>
    {
        public PostTransactionRequestValidator()
        {
        }
    }
}
