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

        public string? Name { get; init; }    

        public string Role { get; init; }

        public Auth()
        {
            Id = Db.NewId();
        }

        public Roles ConcreteRole() => new(Role);

        public Auth(string email, string? name, Roles role)
        {
            Id = Db.NewId();
            Email = email;
            Name = name;
            Role = role.Value;
        }
    }
}
