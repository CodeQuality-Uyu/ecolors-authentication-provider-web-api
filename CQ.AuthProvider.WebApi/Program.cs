using CQ.AuthProvider.WebApi.AppConfig;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.DataAccess.EfCore;
using CQ.IdentityProvider.EfCore;

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
    //.ConfigureApiBehaviorOptions(options =>
    //{
    //    options.InvalidModelStateResponseFactory = context =>
    //    {
    //        var errors = context
    //        .ModelState
    //        .SelectMany(x => x.Value.Errors)
    //        .Select(x => x.ErrorMessage)
    //        .ToList();

    //        var errorResponse = new
    //        {
    //            InnerCode = "RequestInvalid",
    //            Message = "There is some problem with the request or with the values",
    //            Errors = errors
    //        };

    //        return new BadRequestObjectResult(errorResponse);
    //    };
    //});

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

app.UseCors(policy =>
policy
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());

app.MapControllers();

app.Run();

