using CQ.AuthProvider.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IIdentityProviderRepository
    {
        Task CreateAsync(Identity identity);

        Task UpdatePasswordAsync(string identityId, string newPassword);
    }
}
