using FluentValidation;

namespace TransactionsService.Core.Models.Configuration
{
    public class ConnectionStrings
    {
        public const string ConfigSection = "ConnectionStrings";

        public string? TransactionsDbConnectionString { get; init; }
    }

    public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
    {
        public ConnectionStringsValidator()
        {
            RuleFor(model => model.TransactionsDbConnectionString)
                .NotEmpty().WithMessage($"{nameof(ConnectionStrings.TransactionsDbConnectionString)} is required");
        }
    }
}
