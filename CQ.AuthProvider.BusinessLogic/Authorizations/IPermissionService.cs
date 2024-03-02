using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllAsync();

        Task CreateAsync(CreatePermission permission);
    }
}
