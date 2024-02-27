
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    public sealed record class ResetPasswordApplication
    {
        public string Id { get; set; } = null!;

        public MiniAccount Account { get; set; } = null!;

        public string Code { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public ResetPasswordApplication()
        {
            this.Id = Db.NewId();
            this.Code = NewCode();
            this.CreatedAt = DateTimeOffset.Now;
        }

        public ResetPasswordApplication(MiniAccount account) : this()
        {
            this.Account = account;
        }

        public bool HasExpired()
        {
            return DateTimeOffset.UtcNow > this.CreatedAt.AddMinutes(15);
        }

        public static string NewCode()
        {
            return new Random().Next(111, 1111).ToString();
        }
    }

    public sealed record class MiniAccount
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public MiniAccount()
        {
        }

        public MiniAccount(string id, string email)
        {
            this.Id = id;
            this.Email = email;
        }
    }
}
