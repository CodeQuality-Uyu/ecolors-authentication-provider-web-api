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
        public string Id { get; set; }

        public string AuthId { get; init; }

        public string Email { get; init; }

        public string Token { get; init; }

        public Session() 
        {
            Id = Db.NewId();
        }

        public Session(string authId, string email, string token)
        {
            Id = Db.NewId();
            AuthId = authId;
            Email = email;
            Token = token;
        }
    }
}
