using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ConfigurationWithOptionsPattern.API.Extensions;
using ConfigurationWithOptionsPattern.API.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddOptionsWithFluentValidation<AccountInquiryApiSettings, 
    AccountInquiryApiSettingsValidator>(AccountInquiryApiSettings.ConfigurationSection);

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
