using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
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
            var authContext = this._contexts.FirstOrDefault(c => c.GetDatabaseInfo().Name == _authOptions.DatabaseName);


            object authDbHealth = new
            {
                Alive = false
            };

            if (Guard.IsNotNull(authContext))
            {
                var authInfo = authContext.GetDatabaseInfo();

                authDbHealth = new
                {
                    Server = authInfo.Provider,
                    DatabaseName = authInfo.Name,
                    Alive = authContext.Ping()
                };
            }

            return new
            {
                v = "2",
                Alive = true,
                Auth = authDbHealth,
                Identity = new
                {
                    Server = _identityProviderHealthService.GetProvider(),
                    Name = _identityProviderHealthService.GetName(),
                    Alive = _identityProviderHealthService.Ping()
                }
            };
        }
    }
}
