using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public interface IAccountInfoRepository
    {
        Task<bool> ExistByEmailAsync(string email);

        Task<AccountInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception;

        Task<AccountInfo> GetInfoByIdAsync(string id);

        Task<AccountInfo> GetInfoByEmailAsync<TException>(string email, TException exception)
            where TException : Exception;
    }
}
