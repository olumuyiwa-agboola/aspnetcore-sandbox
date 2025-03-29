namespace VerticalSliceArchitecture.API.Extensions
{
    /// <summary>
    /// Contains the <see cref="MountMiddlewares"/> method which adds 
    /// the required middlewares to the application's request pipeline.
    /// </summary>
    internal static class WebApplicationExtension
    {
        /// <summary>
        /// Adds the required middlewares to the application's request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        internal static void MountMiddlewares(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseCors();

            app.UseAuthentication();

            //app.UseAuthorization();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        internal static void MapMinimalEndpoints(this WebApplication app)
        {
            app.MapGroup("CrudApis")
                .MapCrudApiEndpoints()
                .WithTags("CrudApis");
        }
    }
}