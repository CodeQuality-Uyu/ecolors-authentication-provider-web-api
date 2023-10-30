using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("/", Name = "Ping")]
    [Route("health", Name = "HealthCheck")]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public object Get()
        {
            return new
            {
                Alive = true,
            };
        }
    }
}
