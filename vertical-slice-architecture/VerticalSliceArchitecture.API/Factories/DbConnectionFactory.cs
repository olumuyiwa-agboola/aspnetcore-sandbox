using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using VerticalSliceArchitecture.API.Models;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Factories
{
    public class DbConnectionFactory(IOptions<ConnectionStrings> connectionStrings) : IDbConnectionFactory
    {
        public IDbConnection GetStaffRatingsDbConnection()
        {
            return new SqlConnection(connectionStrings.Value.StaffRatingsDbConnectionString);
        }
    }
}
