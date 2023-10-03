using PlayerFinder.Auth.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core
{
    internal interface IUserService
    {
        Task<User> CreateAsync(User user);

        Task<User?> GetByEmailAsync(string email);
    }
}
