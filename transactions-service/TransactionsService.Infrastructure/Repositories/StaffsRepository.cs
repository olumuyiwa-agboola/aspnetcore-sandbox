using Dapper;
using System.Data;
using TransactionsService.Core.Models;
using TransactionsService.Core.Abstractions;

namespace TransactionsService.API.Repositories
{
    public class StaffsRepository(IDbConnectionFactory _dbConnectionFactory) : IStaffsRepository
    {
        public async Task<Staff?> GetStaffByUsername(string username)
        {
            DynamicParameters parameters = new();
            parameters.Add("Username", username);
            string command = "GetStaffByUsername";

            using IDbConnection dbConnection = _dbConnectionFactory.GetStaffRatingsDbConnection();

            return await dbConnection.QueryFirstOrDefaultAsync<Staff>(command, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<Staff?> InsertNewStaffRecord(string username)
        {
            DynamicParameters parameters = new();
            parameters.Add("Username", username);
            string command = "InsertNewStaffRecord";

            using IDbConnection dbConnection = _dbConnectionFactory.GetStaffRatingsDbConnection();

            return await dbConnection.QueryFirstOrDefaultAsync<Staff>(command, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<Staff?> UpdateLastLogin(string username)
        {
            DynamicParameters parameters = new();
            parameters.Add("Username", username);
            string command = "UpdateLastLogin";

            using IDbConnection dbConnection = _dbConnectionFactory.GetStaffRatingsDbConnection();

            return await dbConnection.QueryFirstOrDefaultAsync<Staff>(command, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
