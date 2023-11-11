using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal interface IRoleInternalService : IRoleService
    {
        Task CheckExistAsync(Roles key);

        Task<Role> GetByKeyAsync(Roles key);
    }
}
