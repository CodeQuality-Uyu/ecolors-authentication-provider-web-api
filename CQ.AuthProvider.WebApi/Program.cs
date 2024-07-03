using CQ.AuthProvider.WebApi.AppConfig;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(
    (options) =>
    {
        options.AddExceptionGlobalHandler<CqExceptionFilter>();
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Avoids returning an automatic response when missing a prop in the body
        options.SuppressModelStateInvalidFilter = true;
    });

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services
    .ConfigureApiServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy =>
policy
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());

app.MapControllers();

app.Run();
