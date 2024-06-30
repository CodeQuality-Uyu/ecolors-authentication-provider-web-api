using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;

using AuthEfCoreDbContext = CQ.AuthProvider.DataAccess.EfCore.AuthDbContext;
using AuthMongoDbContext = CQ.AuthProvider.DataAccess.Mongo.AuthDbContext;

namespace CQ.AuthProvider.WebApi.Controllers.Health;

[ApiController]
[Route("/", Name = "Ping")]
[Route("health", Name = "Health Check")]
public class HealthController(
    AuthEfCoreDbContext authEfCoreContext,
    AuthMongoDbContext authMongoContext,
    IIdentityProviderHealthService identityHealthService)
    : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        var authDataBase = new
        {
            Provider = string.Empty,
            Alive = false
        };

        if (Guard.IsNotNull(authEfCoreContext))
        {
            authDataBase = new
            {
                Provider = "EfCore",
                Alive = authEfCoreContext.Ping()
            };
        }

        if (Guard.IsNotNull(authMongoContext))
        {
            authDataBase = new
            {
                Provider = "Mongo",
                Alive = authMongoContext.Ping()
            };
        }

        return new
        {
            v = "3",
            Alive = true,
            Auth = authDataBase,
            Identity = new
            {
                Server = identityHealthService.GetProvider(),
                Alive = identityHealthService.Ping()
            }
        };
    }
}
