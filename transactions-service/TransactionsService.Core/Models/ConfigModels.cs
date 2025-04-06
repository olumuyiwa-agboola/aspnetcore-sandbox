namespace TransactionsService.Core.Models
{
    public class ConnectionStrings
    {
        public const string ConfigSection = "ConnectionStrings";

        public string? StaffRatingsDbConnectionString { get; init; }
    }
}
