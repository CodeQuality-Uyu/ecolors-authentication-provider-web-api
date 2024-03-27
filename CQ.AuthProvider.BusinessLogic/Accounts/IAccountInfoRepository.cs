using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public interface IAccountInfoRepository
    {
        Task<Account> GetInfoByIdAsync(string id);
    }
}
