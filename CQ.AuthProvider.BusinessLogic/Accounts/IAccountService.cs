using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public interface IAccountService
    {
        Task<CreateAccountResult> CreateAsync(CreateAccount auth);

        Task<AccountInfo> GetMeAsync(string token);

        Task<AccountInfo> GetByEmailAsync(string email);
    }
}
