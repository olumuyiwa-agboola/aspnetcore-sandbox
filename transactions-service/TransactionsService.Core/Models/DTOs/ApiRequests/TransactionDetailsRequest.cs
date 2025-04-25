using FluentValidation;

namespace TransactionsService.Core.Models.DTOs.ApiRequests
{
    public record TransactionDetailsRequest(string Reference);

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
}
