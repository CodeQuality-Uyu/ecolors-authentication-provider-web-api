// using CQ.ApiFilters.Config;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.Config;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.
// builder.Services.AddExceptionFilter<CQExceptionFilter>();

// Avoids returning an automatic response when missing a prop in the body
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddCQAuthProviderServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
