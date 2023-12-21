
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IResetPasswordService
    {
        Task<ResetPasswordApplication> CreateAsync(string email);

        Task AcceptAsync(string id, ResetPasswordApplicationAccepted request);

        Task<ResetPasswordApplication> GetActiveByIdAsync(string id);
    }
}
