using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class Auth
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string? Name { get; init; }

        public Auth()
        {
            Id = Db.NewId();
        }

        public Auth(string email, string password, string? name)
        {
            Id = Db.NewId();
            Email = email;
            Password = password;
            Name = name;
        }
    }
}
