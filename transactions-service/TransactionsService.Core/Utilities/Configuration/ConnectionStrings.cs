namespace TransactionsService.Core.Utilities.Configuration
{
    public class ConnectionStrings
    {
        public const string ConfigSection = "ConnectionStrings";

        public string? TransactionsDbConnectionString { get; init; }
    }
}
