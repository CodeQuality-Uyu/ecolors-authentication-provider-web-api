using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.ClientSystems
{
    [ApiController]
    [Route("client-systems")]
    [CQAuthorization]
    [ValidateAccount]
    public sealed class ClientSystemController : ControllerBase
    {
        private readonly IClientSystemService _clientSystemService;

        public ClientSystemController(IClientSystemService clientSystemService)
        {
            this._clientSystemService = clientSystemService;
        }

        [HttpPost]
        public async Task<CreateClientSystemResponse> CreateAsync(CreateClientSystemRequest request)
        {
            var entity = request.Map();

            var privateKey = await this._clientSystemService.CreateAsync(entity).ConfigureAwait(false);

            return new CreateClientSystemResponse(privateKey);
        }
    }
}
