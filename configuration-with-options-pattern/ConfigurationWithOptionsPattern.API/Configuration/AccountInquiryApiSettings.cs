public class AccountInquiryApiSettings
{
    public const string ConfigurationSection = "AccountInquiryApiSettings";

    public string BaseUrl { get; init; }
    public string RetailCustomerDetailsEndpoint { get; init; }
    public string CorporateCustomerDetailsEndpoint { get; init; }
}