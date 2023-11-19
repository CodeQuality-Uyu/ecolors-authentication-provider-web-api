using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IRoleService
    {
        Task<IList<MiniRole>> GetAllPublicAsync();

        Task<IList<Role>> GetAllAsync();

        Task CreateAsync(CreateRole role);

        Task AddPermissionByIdAsync(string id, AddPermission permissions);
    }
}
