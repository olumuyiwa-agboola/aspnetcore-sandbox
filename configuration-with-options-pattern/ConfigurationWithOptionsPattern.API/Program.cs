using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ConfigurationWithOptionsPattern.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/account-inquiry-api-settings", ([FromServices] IOptions<AccountInquiryApiSettings> accountInquiryApiOptions) =>
{
    return Results.Ok(accountInquiryApiOptions.Value);
})
.WithName("GetAccountInquirySettings");

app.Run();
