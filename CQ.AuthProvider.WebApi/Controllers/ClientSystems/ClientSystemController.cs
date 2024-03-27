using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.ClientSystems
{
    [ApiController]
    [Route("client-systems")]
    [CQAuthentication]
    public sealed class ClientSystemController : ControllerBase
    {
        private readonly IClientSystemService _clientSystemService;

        public ClientSystemController(IClientSystemService clientSystemService)
        {
            this._clientSystemService = clientSystemService;
        }

        [HttpPost]
        [CQAuthorization]
        public async Task<CreateClientSystemResponse> CreateAsync(CreateClientSystemRequest request)
        {
            var entity = request.Map();

            var privateKey = await this._clientSystemService.CreateAsync(entity).ConfigureAwait(false);

            return new CreateClientSystemResponse(privateKey);
        }
    }
}
