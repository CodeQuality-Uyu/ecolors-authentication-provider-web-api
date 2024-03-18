using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal interface IPermissionInternalService<TPermission> : IPermissionService
        where TPermission : class
    {
        Task AssertByKeysAsync(List<PermissionKey> permissionKeys);

        Task<List<TPermission>> GetAllByKeysAsync(List<PermissionKey> keys);
    }
}
