using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public interface IPermissionRepository<TPermission> : IRepository<TPermission>
        where TPermission : class
    {
        Task<List<Permission>> GetAllInfoAsync();

        Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys);

        Task<bool> ExistByKeyAsync(PermissionKey permissionKey);
    }
}
