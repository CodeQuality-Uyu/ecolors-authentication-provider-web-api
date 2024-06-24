using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    public sealed record class ResetPasswordApplicationEfCore
    {
        public string Id { get; init; } = null!;

        public string AccountId { get; init; } = null!;

        public AccountEfCore Account { get; init; } = null!;

        public string Code { get; init; } = null!;

        public DateTimeOffset CreatedAt { get; init; }

        public ResetPasswordApplicationEfCore()
        {
            this.Id = Db.NewId();
            this.Code = ResetPassword.NewCode();
            this.CreatedAt = DateTimeOffset.Now;
        }

        public ResetPasswordApplicationEfCore(string accountId)
            : this()
        {
            this.AccountId = accountId;
        }
    }
}
