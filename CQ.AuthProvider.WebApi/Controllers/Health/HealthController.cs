using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.WebApi.AppConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AuthEfCoreDbContext = CQ.AuthProvider.DataAccess.EfCore.AuthDbContext;

namespace CQ.AuthProvider.WebApi.Controllers.Health;

[ApiController]
[Route("/", Name = "Ping")]
[Route("health", Name = "Health Check")]
public class HealthController(
    AuthEfCoreDbContext authEfCoreContext,
    IOptions<DatabaseEngineSection> databaseEngineSectionOptions,
    IIdentityProviderHealthService identityHealthService)
    : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        var databaseEngineSection = databaseEngineSectionOptions.Value;

        return new
        {
            v = "1.8",
            Alive = true,
            Auth = new
            {
                Provider = databaseEngineSection.Auth,
                Alive = authEfCoreContext.Ping()
            },
            Identity = new
            {
                Server = databaseEngineSection.Identity,
                Alive = identityHealthService.Ping()
            }
        };
    }
}
