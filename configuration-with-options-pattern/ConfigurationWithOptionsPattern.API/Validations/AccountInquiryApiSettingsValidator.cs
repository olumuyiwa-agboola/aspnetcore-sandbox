using FluentValidation;

namespace ConfigurationWithOptionsPattern.API.Validations
{
    public class AccountInquiryApiSettingsValidator : AbstractValidator<AccountInquiryApiSettings>
    {
        public AccountInquiryApiSettingsValidator()
        {
            RuleFor(model => model.BaseUrl)
                .NotEmpty().WithMessage($"{nameof(AccountInquiryApiSettings.BaseUrl)} is required")
                .Must(baseUrl => Uri.TryCreate(baseUrl, UriKind.Absolute, out _)).WithMessage($"{nameof(AccountInquiryApiSettings.BaseUrl)} must be a valid URL");

            RuleFor(model => model.RetailCustomerDetailsEndpoint)
                .NotEmpty().WithMessage($"{nameof(AccountInquiryApiSettings.RetailCustomerDetailsEndpoint)} is required");

            RuleFor(model => model.CorporateCustomerDetailsEndpoint)
                .NotEmpty().WithMessage($"{nameof(AccountInquiryApiSettings.CorporateCustomerDetailsEndpoint)} is required");
        }
    }
}
