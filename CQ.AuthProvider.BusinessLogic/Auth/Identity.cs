using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class Identity
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public Identity()
        {
            Id = Db.NewId();
        }

        public Identity(string email, string password) 
            : this()
        {
            Email = email;
            Password = password;
        }
    }
}
