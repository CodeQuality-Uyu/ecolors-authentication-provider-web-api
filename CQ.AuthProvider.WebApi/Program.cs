using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.DataAccess.AppConfig;
using CQ.ServiceExtension;
using CQ.ApiElements.AppConfig;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    (options) =>
    {
        options.AddExceptionFilter<CqExceptionFilter>();
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Avoids returning an automatic response when missing a prop in the body
        options.SuppressModelStateInvalidFilter = true;
    });

// Add services to the container.
builder.Services
.AddHandleException<CQAuthExceptionRegistryService>(LifeTime.Singleton, LifeTime.Singleton)
.AddCQServices(builder.Configuration)
.AddCQDataAccess(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
