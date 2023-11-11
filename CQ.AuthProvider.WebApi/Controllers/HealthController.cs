using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("/", Name = "Ping")]
    [Route("health", Name = "HealthCheck")]
    public class HealthController : ControllerBase
    {
        private readonly IDatabaseContext _authContext;

        private readonly IIdentityProviderHealthService _identityProviderHealthService;

        public HealthController(IDatabaseContext authContext, IIdentityProviderHealthService identityProviderHealthService)
        {
            _authContext = authContext;
            _identityProviderHealthService = identityProviderHealthService;
        }

        [HttpGet]
        public object Get()
        {
            return new
            {
                Alive = true,
                Auth = new
                {
                    Database = this._authContext.GetDatabaseInfo(),
                    Alive = this._authContext.Ping()
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
