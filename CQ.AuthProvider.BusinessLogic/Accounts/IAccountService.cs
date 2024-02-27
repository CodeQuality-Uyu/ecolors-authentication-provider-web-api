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

        Task<Account> GetMeAsync(string token);

        Task<Account> GetByEmailAsync(string email);

        Task<bool> HasPermissionAsync(string permission, Account userLogged);
    }
}
