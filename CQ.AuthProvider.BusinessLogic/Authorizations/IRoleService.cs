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
        Task<List<Role>> GetAllAsync(Account accountLogged, bool isPrivate = false);

        Task CreateAsync(CreateRole role);

        Task CreateBulkAsync(List<CreateRole> roles);

        Task AddPermissionByIdAsync(string id, AddPermission permissions);
    }
}
