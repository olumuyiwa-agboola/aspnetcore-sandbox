using System.Data;

namespace TransactionsService.Core.Abstractions
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetStaffRatingsDbConnection();
    }
}