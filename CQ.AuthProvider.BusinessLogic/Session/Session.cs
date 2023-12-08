using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class Session
    {
        public string Id { get; init; }

        public string AuthId { get; init; }

        public string Email { get; init; }

        public string Token { get; init; }

        public Session() 
        {
            this.Id = Db.NewId();
        }

        public Session(string authId, string email, string token)
        {
            this.Id = Db.NewId();
            this.AuthId = authId;
            this.Email = email;
            this.Token = token;
        }
    }
}
