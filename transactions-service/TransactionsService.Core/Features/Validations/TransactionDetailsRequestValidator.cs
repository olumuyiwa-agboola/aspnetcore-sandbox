using FluentValidation;
using TransactionsService.Core.Models.DTOs;

namespace TransactionsService.Core.Features.Validations
{
    public class TransactionDetailsRequestValidator : AbstractValidator<TransactionDetailsRequest>
    {
        public TransactionDetailsRequestValidator()
        {
            RuleFor(model => model.Reference)
                .NotEmpty().WithMessage($"{nameof(TransactionDetailsRequest.Reference)} is required")
                .Length(30).WithMessage($"{nameof(TransactionDetailsRequest.Reference)} must contain exaclty 30 digits")
                .Matches(@"^\d+$").WithMessage($"{nameof(TransactionDetailsRequest.Reference)} must contain only digits");
        }
    }

    public class PostTransactionRequestValidator : AbstractValidator<PostTransactionRequest>
    {
        public PostTransactionRequestValidator()
        {
            
        }
    }
}
