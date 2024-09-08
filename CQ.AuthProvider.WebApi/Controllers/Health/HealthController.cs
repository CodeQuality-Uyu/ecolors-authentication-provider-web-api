using CQ.AuthProvider.BusinessLogic.Identities;
using Microsoft.AspNetCore.Mvc;

using AuthEfCoreDbContext = CQ.AuthProvider.DataAccess.EfCore.AuthDbContext;

namespace CQ.AuthProvider.WebApi.Controllers.Health;

[ApiController]
[Route("/", Name = "Ping")]
[Route("health", Name = "Health Check")]
public class HealthController(
    AuthEfCoreDbContext authEfCoreContext,
    IIdentityProviderHealthService identityHealthService)
    : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        var authDataBase = new
        {
            Provider = "EfCore",
            Alive = authEfCoreContext.Ping()
        };

        return new
        {
            v = "4",
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
