using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class SessionCreated
    {
        public readonly string AuthId;

        public readonly string Email;

        public readonly string Token;

        public readonly IList<string> Roles;

        public SessionCreated(string authId, string email, string token, IList<string> roles)
        {
            this.AuthId = authId;
            this.Email = email;
            this.Token = token;
            this.Roles = roles;
        }
    }
}
