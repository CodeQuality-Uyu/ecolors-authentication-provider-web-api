using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountMongo
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<AccountRoleMongo> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public AccountMongo()
        {
            this.Id = Db.NewId();
            this.Roles = new List<AccountRoleMongo>();
            this.CreatedAt = DateTimeOffset.UtcNow;
        }

        public AccountMongo(
            string name,
            string email,
            AccountRoleMongo role
            )
            : this()
        {
            this.Name = name;
            this.Email= email;
            this.Roles = new List<AccountRoleMongo> { role };
        }
    }
}
