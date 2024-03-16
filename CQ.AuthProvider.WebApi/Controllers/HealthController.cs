using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("/", Name = "Ping")]
    [Route("health", Name = "HealthCheck")]
    public class HealthController : ControllerBase
    {
        private readonly List<IDatabaseContext> _contexts;

        private readonly IIdentityProviderHealthService _identityProviderHealthService;

        public HealthController(
            IEnumerable<IDatabaseContext> contexts,
            IIdentityProviderHealthService identityProviderHealthService)
        {
            _contexts = contexts.ToList();
            _identityProviderHealthService = identityProviderHealthService;
        }

        [HttpGet]
        public object Get()
        {
            var authContext = this._contexts.FirstOrDefault(c => c.GetDatabaseInfo().Name == "Auth");
            return new
            {
                Alive = true,
                Auth = new
                {
                    Database = authContext.GetDatabaseInfo(),
                    Alive = authContext.Ping()
                },
                Identity = new
                {
                    Service = this._identityProviderHealthService.GetName(),
                    Alive = this._identityProviderHealthService.Ping()
                }
            };
        }
    }
}
