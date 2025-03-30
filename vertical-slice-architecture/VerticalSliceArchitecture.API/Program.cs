using VerticalSliceArchitecture.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddFluentValidators();
builder.Services.AddApplicationOptions();
builder.Services.AddOpenApiDocumentation();

builder.Host.ConfigureSerilogLogger();

var app = builder.Build();

app.ConfigureRequestPipeline();

app.Run();