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

        public AuthService(IAuthRepository authRepRepository)
        {
            _authRepRepository = authRepRepository;
        }

        public async Task<CreateAuthResult> CreateAsync(CreateAuth newAuth)
        {
            await AssertEmailInUse(newAuth.Email).ConfigureAwait(false);

            var auth = new Auth(
                newAuth.Email,
               newAuth.Password,
               newAuth.FullName());

            var authCreated = await _authRepRepository.CreateAsync(auth).ConfigureAwait(false);

            return authCreated;
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
            var updated = userLogged with { Password= newPassword };

            await _authRepRepository.UpdateAsync(updated).ConfigureAwait(false);
        }
    }
}
