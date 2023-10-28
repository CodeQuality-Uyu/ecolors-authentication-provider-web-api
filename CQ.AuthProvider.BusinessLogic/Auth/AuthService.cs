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
        private readonly IAuthRepository _authRepRepository;

        private readonly ISessionService _sessionService;

        public AuthService(IAuthRepository authRepRepository, ISessionService sessionService)
        {
            _authRepRepository = authRepRepository;
            _sessionService = sessionService;
        }

        public async Task<CreateAuthResult> CreateAsync(CreateAuth newAuth)
        {
            await AssertEmailInUse(newAuth.Email).ConfigureAwait(false);

            var auth = new Auth(
                newAuth.Email,
               newAuth.Password,
               newAuth.FullName());

            var authCreated = await _authRepRepository.CreateAsync(auth).ConfigureAwait(false);

            var session = await _sessionService.CreateAsync(new CreateSessionCredentials(newAuth.Email, newAuth.Password)).ConfigureAwait(false);

            return new CreateAuthResult(authCreated.Id, authCreated.Email, authCreated.Name, session.Token);
        }

        private async Task AssertEmailInUse(string email)
        {
            var existAuth = await _authRepRepository.ExistByEmailAsync(email).ConfigureAwait(false);

            if (existAuth)
            {
                throw new DuplicatedEmailException(email);
            }
        }

        public async Task UpdatePasswordAsync(string newPassword, Auth userLogged)
        {
            var updated = userLogged with { Password = newPassword };

            await _authRepRepository.UpdateAsync(updated).ConfigureAwait(false);
        }
    }
}
