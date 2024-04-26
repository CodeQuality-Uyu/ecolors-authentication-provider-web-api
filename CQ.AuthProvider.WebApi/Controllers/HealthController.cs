using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
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

        private readonly AuthOptions _authOptions;

        public HealthController(
            IEnumerable<IDatabaseContext> contexts,
            IIdentityProviderHealthService identityProviderHealthService,
            AuthOptions authOptions)
        {
            _contexts = contexts.ToList();
            _identityProviderHealthService = identityProviderHealthService;
            _authOptions = authOptions;
        }

        [HttpGet]
        public object Get()
        {
            var authContext = this._contexts.FirstOrDefault(c => c.GetDatabaseInfo().Name == _authOptions.AuthDatabaseName);

            return new
            {
                v = "1",
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
