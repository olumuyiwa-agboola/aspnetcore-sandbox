using FluentValidation;
using VerticalSliceArchitecture.API.Models;

namespace VerticalSliceArchitecture.API.Validations
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