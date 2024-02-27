using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Identities
{
    public sealed record class Identity
    {
        public string Id { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;

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
