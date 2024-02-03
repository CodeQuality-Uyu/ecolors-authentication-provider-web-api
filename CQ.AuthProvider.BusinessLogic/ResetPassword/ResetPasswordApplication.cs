
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed class ResetPasswordApplication
    {
        public string Id { get; set; } = Db.NewId();

        public MiniAuth Auth { get; set; } = null!;

        public string Code { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ResetPasswordApplication()
        {
            this.Code = NewCode();
        }

        public ResetPasswordApplication(MiniAuth auth) : this()
        {
            this.Auth = auth;
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

    public sealed class MiniAuth {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public MiniAuth()
        {
        }

        public MiniAuth(string id, string email)
        {
            this.Id = id;
            this.Email = email;
        }
    }
}
