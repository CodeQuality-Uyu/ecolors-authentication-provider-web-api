using CQ.AuthProvider.WebApi.AppConfig;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.DataAccess.EfCore;
using CQ.IdentityProvider.EfCore;
using CQ.Extensions.Configuration;
using CQ.Utility;

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
    .ConfigureApiServices(configuration);

var app = builder.Build();

// Config missing migrations
app
    .Services
    .AddDbContextMissingMigrations<AuthDbContext>(app.Environment)
    .AddDbContextMissingMigrations<IdentityDbContext>(app.Environment);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();


var origins = app.Configuration.GetSection<string[]>("Cors:Origins");
Guard.ThrowIsNullOrEmpty(origins?.ToList(), "Cors:Origins");
app.UseCors(policy =>
policy
.WithOrigins(origins)
.AllowAnyHeader()
.AllowAnyMethod());

app.MapControllers();

app.Run();

