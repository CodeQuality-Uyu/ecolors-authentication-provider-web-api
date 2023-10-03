using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IAuthService
    {
        Task<Auth> CreateAsync(CreateAuth auth);

        Task<string> LoginAsync(string email, string password);

        Task<Auth> DeserializeTokenAsync(string token);

        Task UpdatePasswordAsync(string password, Auth userLogged);

        Task<Auth> GetByEmailAsync(string email);
    }
}
