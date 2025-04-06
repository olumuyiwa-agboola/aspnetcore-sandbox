using Scalar.AspNetCore;

namespace TransactionsService.API.Extensions
{
    /// <summary>
    /// Contains the <see cref="ConfigureRequestPipeline"/> method which adds 
    /// the required middlewares and endpoints to the application's request pipeline.
    /// </summary>
    internal static class WebApplicationExtension
    {
        /// <summary>
        /// Adds the required middlewares and endpoints to the application's request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        internal static void ConfigureRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.DefaultFonts = false;
                    options.Title = "Staff Rating API";
                });

                app.MapGet("/", () => Results.Redirect("/scalar/v1"))
                    .ExcludeFromDescription();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.MapGroup("Staff")
                .MapStaffsEndpoints()
                .WithTags("Staff");
        }
    }
}