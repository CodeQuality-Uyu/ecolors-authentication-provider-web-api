using PlayerFinder.Auth.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core
{
    public interface IAuthService
    {
        Task<User> CreateAsync(AuthUser auth);

        Task<string> LoginAsync(string email, string password);

        Task<User> DeserializeTokenAsync(string token);

        Task UpdatePasswordAsync(string password, User userLogged);

        Task<User> GetByEmailAsync(string email);
    }
}
