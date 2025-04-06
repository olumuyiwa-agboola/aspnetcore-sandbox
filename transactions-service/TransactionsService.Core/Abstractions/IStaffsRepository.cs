using TransactionsService.Core.Models;

namespace TransactionsService.Core.Abstractions
{
    public interface IStaffsRepository
    {
        Task<Staff?> UpdateLastLogin(string username);

        Task<Staff?> GetStaffByUsername(string username);

        Task<Staff?> InsertNewStaffRecord(string username);
    }
}
