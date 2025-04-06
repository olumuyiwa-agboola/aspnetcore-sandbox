using FluentValidation;
using TransactionsService.Core.Models;

namespace TransactionsService.Core.Features.Validations
{
    public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
    {
        public ConnectionStringsValidator()
        {
            RuleFor(model => model.StaffRatingsDbConnectionString)
                .NotEmpty().WithMessage($"{nameof(ConnectionStrings.StaffRatingsDbConnectionString)} is required");
        }
    }
}