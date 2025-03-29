using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API.Extensions
{
    public static class RouteHandlerBuilderExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstErrorOnly"></param>
        /// <returns></returns>
        public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder, bool firstErrorOnly = true)
        {
            builder.AddEndpointFilter(async (invocationContext, next) =>
            {
                if (!invocationContext.HttpContext.Request.Path.ToString().StartsWith("/swagger"))
                {
                    var argument = invocationContext.Arguments.OfType<T>().FirstOrDefault();
                    var response = argument!.DataAnnotationsValidate();

                    if (!response.IsValid)
                    {
                        string errorMessage = firstErrorOnly ?
                                                response.Results.FirstOrDefault()!.ErrorMessage! :
                                                string.Join("|", response.Results.Select(x => x.ErrorMessage));

                        return Results.Problem(errorMessage, statusCode: 400);
                    }
                }

                return await next(invocationContext);
            });

            return builder;
        }

        private static (List<ValidationResult> Results, bool IsValid) DataAnnotationsValidate(this object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            var isValid = Validator.TryValidateObject(model, context, results, true);

            return (results, isValid);
        }
    }
}