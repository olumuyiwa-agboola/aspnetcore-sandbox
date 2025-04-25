using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace TransactionsService.Core.Factories
{
    public class ProblemDetailsFactory
    {
        public static ProblemDetails CreateBadRequestResponseFromFluentValidationResult(List<ValidationFailure> validationFailures)
        {
            Dictionary<string, string> errorDictionary = [];
            foreach (var failure in validationFailures)
            {
                if (errorDictionary.ContainsKey(failure.PropertyName))
                    errorDictionary[failure.PropertyName] += " | " + failure.ErrorMessage;
                else
                    errorDictionary.Add(failure.PropertyName, failure.ErrorMessage);
            }

            ProblemDetails badRequestResponse = new()
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = $"One or more validations failed."
            };

            badRequestResponse.Extensions.Add("Errors", errorDictionary);

            return badRequestResponse;
        }

        public static ProblemDetails CreateNotFoundResponse(string entity, string identifierName, string identifier)
        {
            return new ProblemDetails()
            {
                Title = $"{entity} Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"{entity} with {identifierName} {identifier} was not found."
            };
        }

        public static ProblemDetails CreateInternalServerErrorResponse(string detail)
        {
            return new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = $"{detail}."
            };
        }
    }
}
