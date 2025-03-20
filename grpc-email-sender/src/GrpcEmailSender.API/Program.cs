using GrpcEmailSender.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<EmailSenderService>();
app.MapGrpcReflectionService();

app.Run();