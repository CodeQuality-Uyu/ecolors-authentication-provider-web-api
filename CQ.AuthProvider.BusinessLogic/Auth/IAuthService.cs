using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IAuthService
    {
        Task<CreateAuthResult> CreateAsync(CreateAuth auth);

        Task<Auth> GetMeAsync(string token);

        Task<bool> HasPermissionAsync(string permission, Auth userLogged);
    }
}
