namespace VerticalSliceArchitecture.API.Endpoints
{
    public class Login
    {
        internal async static Task<IResult> HandleRequest()
        {
            return TypedResults.Ok("Hello, world!");
        }
    }
}
