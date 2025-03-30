using System.Data;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetStaffRatingsDbConnection();
    }
}