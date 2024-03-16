using CQ.AuthProvider.BusinessLogic.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public interface IRoleService
    {
        Task<List<RoleInfo>> GetAllAsync(bool isPrivate, AccountInfo accountLogged);

        Task CreateAsync(CreateRole role);

        Task AddPermissionByIdAsync(string id, AddPermission permissions);
    }
}
