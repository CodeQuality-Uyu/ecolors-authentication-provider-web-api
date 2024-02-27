using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllAsync();

        Task<List<MiniPublicPermission>> GetAllPublicAsync();

        Task CreateAsync(CreatePermission permission);
    }
}
