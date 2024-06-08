using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.WebApi.Filters;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Extensions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    [ApiController]
    [Route("sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("credentials")]
        public async Task<CreateSessionResponse> CreateAsync(CreateSessionCredentialsRequest request)
        {
            var createAuth = request.Map();

            var session = await _sessionService.CreateAsync(createAuth);

            var response = new CreateSessionResponse(session);

            return response;
        }

        [HttpDelete("credentials")]
        [CQAuthorization]
        [ValidateAccount]
        public async Task DeleteAsync()
        {
            var accountLogged = this.GetAccountLogged();

            await _sessionService.DeleteAsync(accountLogged);
        }


        [HttpGet("{token}/validate")]
        [CQAuthorization]
        [ValidateClientSystem]
        public async Task<TokenValidationResponse> ValidateTokenAsync(string token)
        {
            var isValid = await _sessionService.IsTokenValidAsync(token).ConfigureAwait(false);

            return new TokenValidationResponse(isValid);
        }
    }
}
