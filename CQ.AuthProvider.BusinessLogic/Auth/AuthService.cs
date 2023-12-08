using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal sealed class AuthService : IAuthService
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        private readonly IRepository<Auth> _authRepository;

        private readonly ISessionService _sessionService;

        private readonly IRoleInternalService _roleService;

        public AuthService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IRepository<Auth> cqAuthRepository,
            IRoleInternalService roleService)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._authRepository = cqAuthRepository;
            this._roleService = roleService;
        }

        public async Task<CreateAuthResult> CreateAsync(CreateAuth newAuth)
        {
            await AssertEmailInUse(newAuth.Email).ConfigureAwait(false);

            await this._roleService.CheckExistAsync(newAuth.Role).ConfigureAwait(false); 

            var auth = await SaveAuthAsync(newAuth).ConfigureAwait(false);

            var session = await _sessionService.CreateAsync(new CreateSessionCredentials(newAuth.Email, newAuth.Password)).ConfigureAwait(false);

            return new CreateAuthResult(auth.Id, auth.Email, auth.Name, session.Token);
        }

        private async Task AssertEmailInUse(string email)
        {
            var existAuth = await this._authRepository.ExistAsync(a => a.Email == email).ConfigureAwait(false);

            if (existAuth)
            {
                throw new ResourceDuplicatedException(nameof(Auth.Email), email, nameof(Auth));
            }
        }

        private async Task<Auth> SaveAuthAsync(CreateAuth newAuth)
        {
            var identity = new Identity(
                newAuth.Email,
                newAuth.Password);

            await _identityProviderRepository.CreateAsync(identity).ConfigureAwait(false);

            var auth = new Auth(newAuth.Email, newAuth.FullName(), newAuth.Role)
            {
                Id = identity.Id
            };

            await this._authRepository.CreateAsync(auth).ConfigureAwait(false);

            return auth;
        }

        public async Task UpdatePasswordAsync(string newPassword, Auth userLogged)
        {
            Guard.ThrowIsInputInvalidPassword(newPassword, "newPassword");

            await _identityProviderRepository.UpdatePasswordAsync(newPassword, userLogged.Id).ConfigureAwait(false);
        }

        public async Task<Auth> GetMeAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var auth = await this._authRepository.GetByPropAsync(
                session.AuthId,
                new SpecificResourceNotFoundException<Session>(nameof(Session.AuthId), session.AuthId))
                .ConfigureAwait(false);

            return auth;
        }

        public async Task<bool> HasPermissionAsync(string permission, Auth userlogged)
        {
            return await this._roleService.HasPermissionAsync(userlogged.ConcreteRoles(), permission).ConfigureAwait(false);
        }
    }
}
