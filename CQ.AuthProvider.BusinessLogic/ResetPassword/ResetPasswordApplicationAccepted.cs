using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class ResetPasswordApplicationAccepted
    {
        public readonly string NewPassword = null!;

        public readonly string Code = null!;

        public readonly string Email = null!;

        public ResetPasswordApplicationAccepted(string newPassword, string code, string email)
        {
            this.NewPassword = Guard.Encode(newPassword.Trim());
            this.Code = Guard.Encode(code.Trim());
            this.Email = Guard.Encode(email.Trim());

            Guard.ThrowIsInputInvalidEmail(this.Email);
            Guard.ThrowIsInputInvalidPassword(this.NewPassword);
            Guard.ThrowIsNullOrEmpty(this.Code, nameof(this.Code));
        }
    }
}
