using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    [ApiController]
    [Route("sessions")]
    public class SessionController: ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            this._sessionService = sessionService;
        }

        [HttpPost("credentials")]
        public async Task<CreateSessionResponse> CreateAsync(CreateSessionCredentialsRequest request)
        {
            var createAuth = request.Map();

            var session = await this._sessionService.CreateAsync(createAuth);

            var response = new CreateSessionResponse(session);

            return response;
        }

        [HttpGet("{token}/validate")]
        public async Task<TokenValidationResponse> ValidateTokenAsync(string token)
        {
            var isValid = await this._sessionService.IsTokenValidAsync(token).ConfigureAwait(false);

            return new TokenValidationResponse(isValid);
        }
    }
}
