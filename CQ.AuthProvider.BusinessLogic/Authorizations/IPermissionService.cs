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
        Task<List<Permission>> GetAllAsync(Account accountLogged, bool isPrivate = false, string? roleId = null);

        Task CreateAsync(CreatePermission permission);

        Task CreateBulkAsync(List<CreatePermission> permission);
    }
}
