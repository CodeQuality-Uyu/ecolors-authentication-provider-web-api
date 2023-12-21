
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class ResetPasswordApplication
    {
        public string Id { get; init; }

        public MiniAuth Auth { get; init; }

        public string Code { get; init; }

        public DateTime CreatedAt { get; init; }

        public ResetPasswordApplication()
        {
            this.Id = Db.NewId();
            this.Code = new Random().Next(111, 1111).ToString();
            this.CreatedAt = DateTime.UtcNow;
        }

        public ResetPasswordApplication(MiniAuth auth) : this()
        {
            this.Auth = auth;
        }

        public bool HasExpired()
        {
            return this.CreatedAt.AddMinutes(15) < DateTime.UtcNow;
        }
    }

    public sealed record class MiniAuth(string Id, string Email);
}
