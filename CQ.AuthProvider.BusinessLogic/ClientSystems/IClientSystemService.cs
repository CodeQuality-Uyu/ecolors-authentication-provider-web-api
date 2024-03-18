using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    public interface IClientSystemService
    {
        Task<ClientSystem> GetByPrivateKeyAsync(string privateKey);

        Task<string> CreateAsync(string name);
    }
}
