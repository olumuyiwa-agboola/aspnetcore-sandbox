using FluentValidation;
using TransactionsService.Core.Models;

namespace TransactionsService.Core.Features.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(model => model.Username)
                .MaximumLength(100).WithMessage($"{nameof(LoginRequest.Username)} must not be more than 100 characters")
                .Matches(@"^[a-z].[a-z]").WithMessage($"{nameof(LoginRequest.Username)} must be in the the firstName.lastName format");
        }
    }
}
