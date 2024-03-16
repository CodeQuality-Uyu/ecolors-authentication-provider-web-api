using CQ.AuthProvider.BusinessLogic.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllAsync(bool isPrivate, string? roleId, AccountInfo accountLogged);

        Task CreateAsync(CreatePermission permission);
    }
}
