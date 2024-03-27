using AutoMapper;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    internal sealed class ClientSystemMongoService : ClientSystemService
    {
        private readonly IRepository<ClientSystemMongo> _clientSystemRepository;

        private readonly IRoleInternalService _roleService;

        public ClientSystemMongoService(
            IRepository<ClientSystemMongo> clientSystemRepository,
            IRoleInternalService roleService,
            IMapper mapper)
            : base(mapper)
        {
            this._clientSystemRepository = clientSystemRepository;
            this._roleService = roleService;
        }

        public override async Task<string> CreateAsync(string name)
        {
            var role = await this._roleService.GetByKeyAsync(RoleKey.ClientSystem).ConfigureAwait(false);

            var miniRole = new MiniRoleMongo(role.Key, role.Permissions);

            var clientSystem = new ClientSystemMongo(name, miniRole);

            await this._clientSystemRepository.CreateAsync(clientSystem).ConfigureAwait(false);

            return clientSystem.PrivateKey;
        }

        protected override async Task<object> GetByPrivateKeyAsync(string privateKey)
        {
            var clientSystem = await this._clientSystemRepository.GetByPropAsync(privateKey, nameof(ClientSystemMongo.PrivateKey)).ConfigureAwait(false);

            return clientSystem;
        }
    }
}
