namespace TransactionsService.API.Handlers
{
    public class LoginCredentialsHandler
    {
        public static bool Validate(string username, string password)
        {
            return (username == "iniobong.ukpong" && password == "123456");
        }
    }
}
