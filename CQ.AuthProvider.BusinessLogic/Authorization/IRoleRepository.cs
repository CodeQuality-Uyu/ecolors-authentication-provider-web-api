using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task AddPermissionsByIdAsync(string id, List<Permission> permissionsKeys);
    }
}
