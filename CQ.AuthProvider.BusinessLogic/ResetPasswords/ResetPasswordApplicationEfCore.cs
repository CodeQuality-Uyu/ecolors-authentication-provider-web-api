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
        public string Id { get; set; } = null!;

        public string AccountId { get; set; } = null!;

        public AccountEfCore Account { get; set; } = null!;

        public string Code { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public ResetPasswordApplicationEfCore()
        {
            this.Id = Db.NewId();
            this.Code = ResetPasswordApplication.NewCode();
            this.CreatedAt = DateTimeOffset.Now;
        }

        public ResetPasswordApplicationEfCore(string accountId)
            : this()
        {
            this.AccountId = accountId;
        }
    }
}
