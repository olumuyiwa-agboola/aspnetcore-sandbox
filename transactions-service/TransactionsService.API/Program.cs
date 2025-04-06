using TransactionsService.API.Extensions;

var app = WebApplication.CreateBuilder(args)
    .ConfigureServices();

app.ConfigureRequestPipeline();

app.Run();