using VerticalSliceArchitecture.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();

builder.Host.ConfigureSerilogLogger();

var app = builder.Build();

app.MountMiddlewares();

app.MapMinimalEndpoints();

app.Run();