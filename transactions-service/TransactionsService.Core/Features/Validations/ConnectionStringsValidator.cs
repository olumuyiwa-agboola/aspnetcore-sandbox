using FluentValidation;
using TransactionsService.Core.Utilities.Configuration;

namespace TransactionsService.Core.Features.Validations
{
    public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
    {
        public ConnectionStringsValidator()
        {
            RuleFor(model => model.TransactionsDbConnectionString)
                .NotEmpty().WithMessage($"{nameof(ConnectionStrings.TransactionsDbConnectionString)} is required");
        }
    }
}