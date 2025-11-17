using CQ.AuthProvider.WebApi.AppConfig;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.DataAccess.EfCore;
using CQ.IdentityProvider.EfCore;
using CQ.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Config controllers
services
    .AddControllers(
    (options) =>
    {
        options.AddExceptionGlobalHandler();
    });

// Add services to the container.
services
    .ConfigureAutoValidation()
    .ConfigureApiServices(configuration, builder.Environment)
    .ConfigureBlob(configuration, builder.Environment)
    .ConfigHealthChecks(configuration);

var app = builder.Build();

// Config missing migrations
app
    .Services
    .AddDbContextMissingMigrations<AuthDbContext>(app.Environment)
    .AddDbContextMissingMigrations<IdentityDbContext>(app.Environment);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

var allowedOrigins = app.Configuration.GetSection<List<string>>("Cors:Origins") ?? [];
if (allowedOrigins.Count == 0 && !app.Environment.IsProduction())
{
    allowedOrigins.AddRange(["http://localhost:3000", "https://localhost:3000"]);
}

app.UseCors(policy => policy.WithOrigins(allowedOrigins.ToArray()).AllowAnyHeader().AllowAnyMethod());
app.MapHealthChecks(
    "health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapControllers();

app.Run();

