using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TransactionsService.Core.Models;
using TransactionsService.Core.Abstractions;

namespace TransactionsService.Core.Features.Factories
{
    public class DbConnectionFactory(IOptions<ConnectionStrings> connectionStrings) : IDbConnectionFactory
    {
        public IDbConnection GetStaffRatingsDbConnection()
        {
            return new SqlConnection(connectionStrings.Value.StaffRatingsDbConnectionString);
        }
    }
}
