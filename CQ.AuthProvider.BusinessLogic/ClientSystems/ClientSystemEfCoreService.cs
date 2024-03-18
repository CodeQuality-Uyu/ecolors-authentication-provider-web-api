using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    internal sealed class ClientSystemEfCoreService : ClientSystemService
    {
        private readonly IRepository<ClientSystemEfCore> _clientSystemRepository;

        private readonly IRoleInternalService<RoleEfCore> _roleService;

        public ClientSystemEfCoreService(
            IRepository<ClientSystemEfCore> clientSystemRepository,
            IRoleInternalService<RoleEfCore> roleInternalService,
            IMapper mapper)
            : base(mapper)
        {
            this._clientSystemRepository = clientSystemRepository;
            this._roleService = roleInternalService;
        }

        protected override async Task<object> GetByPrivateKeyAsync(string privateKey)
        {
            var clientSystem = await this._clientSystemRepository.GetByPropAsync(privateKey, nameof(ClientSystemEfCore.PrivateKey)).ConfigureAwait(false);

            return clientSystem;
        }

        public override async Task<string> CreateAsync(string name)
        {
            var role = await this._roleService.GetByKeyAsync(RoleKey.ClientSystem).ConfigureAwait(false);

            var clientSystem = new ClientSystemEfCore(name, role.Id);

            await this._clientSystemRepository.CreateAsync(clientSystem).ConfigureAwait(false);

            return clientSystem.PrivateKey;
        }
    }
}
