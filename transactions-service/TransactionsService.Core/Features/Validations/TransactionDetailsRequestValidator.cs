using FluentValidation;
using TransactionsService.Core.Models.DTOs;

namespace TransactionsService.Core.Features.Validations
{
    public class TransactionDetailsRequestValidator : AbstractValidator<TransactionDetailsRequest>
    {
        public TransactionDetailsRequestValidator()
        {
            RuleFor(model => model.Reference)
                .Length(30).WithMessage($"{nameof(TransactionDetailsRequest.Reference)} must contain exaclty 30 characters")
                .Matches(@"^\d+$").WithMessage($"{nameof(TransactionDetailsRequest.Reference)} must contain only digits");
        }
    }
}
