using AutoMapper;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    internal abstract class ClientSystemService : IClientSystemService
    {
        private readonly IMapper _mapper;

        public ClientSystemService(
            IMapper mapper)
        {
            this._mapper = mapper;
        }

        #region GetByPrivateKey
        async Task<ClientSystem> IClientSystemService.GetByPrivateKeyAsync(string privateKey)
        {
            var clientSystem = await this.GetByPrivateKeyAsync(privateKey).ConfigureAwait(false);

            var clientSystemMapped = this._mapper.Map<ClientSystem>(clientSystem);

            return clientSystemMapped;
        }

        protected abstract Task<object> GetByPrivateKeyAsync(string privateKey);
        #endregion

        public abstract Task<string> CreateAsync(string name);
    }
}
