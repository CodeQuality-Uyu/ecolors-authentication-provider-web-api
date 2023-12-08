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

        public IList<string> Roles { get; init; }

        public DateTime CreatedAt { get; init; }

        public Auth()
        {
            Id = Db.NewId();
            CreatedAt= DateTime.UtcNow;
        }

        public Auth(string email, string? name, Roles role)
        {
            this.Id = Db.NewId();
            this.Email = email;
            this.Name = name;
            this.Roles = new List<string> { role.ToString() };
            this.CreatedAt= DateTime.UtcNow;
        }

        public IList<Roles> ConcreteRoles()
        {
            return this.Roles.Select(r => new Roles(r)).ToList();
        }
    }
}
