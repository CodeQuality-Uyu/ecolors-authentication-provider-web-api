using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IRoleService
    {
        Task<IList<MiniRole>> GetAllAsync();

        Task CreateAsync(CreateRole role);
    }
}
