using Microsoft.AspNetCore.Mvc;
using TransactionsService.API.Handlers;
using TransactionsService.Core.Abstractions;
using TransactionsService.Core.Models;

namespace TransactionsService.API.Endpoints
{
    public class Login
    {
        internal async static Task<IResult> HandleRequest([FromBody] LoginRequest loginRequest, [FromServices] IStaffsRepository staffsRepository)
        {
            string username = loginRequest.Username!;
            string password = loginRequest.Password!;

            bool loginCredentialsAreValid = LoginCredentialsHandler.Validate(username, password);

            if (!loginCredentialsAreValid)
                return TypedResults.Unauthorized();

            Staff? staff = await staffsRepository.GetStaffByUsername(username);

            if (staff is not null)
            {
                staff = await staffsRepository.UpdateLastLogin(username);

                return TypedResults.Ok(staff);
            }

            staff = await staffsRepository.InsertNewStaffRecord(username);

            return TypedResults.Ok(staff);
        }
    }
}
