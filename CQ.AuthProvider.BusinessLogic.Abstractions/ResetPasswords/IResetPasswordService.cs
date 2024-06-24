
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    public interface IResetPasswordService
    {
        Task CreateAsync(string email);

        Task AcceptAsync(string id, ResetPasswordApplicationAccepted request);

        Task<ResetPasswordApplication> GetActiveByIdAsync(string id);
    }
}
