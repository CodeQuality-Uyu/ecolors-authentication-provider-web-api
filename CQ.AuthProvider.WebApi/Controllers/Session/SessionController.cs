using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.WebApi.Filters;
using System.ComponentModel.DataAnnotations;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController: ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            this._sessionService = sessionService;
        }

        [HttpPost("credentials")]
        public async Task<IActionResult> CreateAsync(CreateSessionCredentialsRequest request)
        {
            var createAuth = request.Map();

            var session = await this._sessionService.CreateAsync(createAuth);

            return Ok(session);
        }
    }
}
