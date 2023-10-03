using CQ.UnitOfWork.Core;
using PlayerFinder.Auth.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._userRepository = unitOfWork.GetRepository<User>();
        }

        public async Task<User> CreateAsync(User user)
        {
            var userCreated = await this._userRepository.CreateAsync(user).ConfigureAwait(false);

            return userCreated;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await this._userRepository.GetOrDefaultAsync(user => user.Email == email).ConfigureAwait(false);

            return user;
        }
    }
}
