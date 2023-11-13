// using CQ.ApiFilters.Config;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.DataAccess.AppConfig;
using CQ.ServiceExtension;
using CQ.ApiElements.Config;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

builder.Services.AddControllers(
    (options) =>
{
    options.AddExceptionFilter();
})
// Avoids returning an automatic response when missing a prop in the body
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Add services to the container.
builder.Services
    .AddHandleException<CQAuthExceptionRegistryService>(LifeTime.Singleton, LifeTime.Singleton)
    .AddCQServices()
    .AddCQDataAccess();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
