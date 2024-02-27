using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class Account
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Name { get; set; }

        public List<Role> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public Account()
        {
            this.Id = Db.NewId();
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.Roles = new List<Role>();
        }

        public Account(
            string email,
            string? name,
            Role role)
            : this()
        {
            this.Email = email;
            this.Name = name;
            this.Roles = new List<Role> { role };
        }

        public List<RoleKey> ConcreteRoles()
        {
            return this.Roles.Select(r => new RoleKey(r.Key)).ToList();
        }
    }
}
