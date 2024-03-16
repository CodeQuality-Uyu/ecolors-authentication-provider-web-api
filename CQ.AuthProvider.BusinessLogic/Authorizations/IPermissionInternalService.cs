using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal interface IPermissionInternalService : IPermissionService
    {
        Task ExistByKeysAsync(List<PermissionKey> permissionKeys);

        Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> keys);
    }
}
