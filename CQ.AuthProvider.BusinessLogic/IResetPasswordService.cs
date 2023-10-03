using PlayerFinder.Auth.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core
{
    public interface IResetPasswordService
    {
        Task<ResetPasswordRequest> CreateAsync(string email);

        Task AcceptAsync(string id, NewPasswordRequest request);

        Task<ResetPasswordRequest> GetActiveResetPasswordByIdAsync(string id);
    }
}
