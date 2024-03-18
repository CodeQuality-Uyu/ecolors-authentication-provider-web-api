using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    public sealed class ClientSystemMongo
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public string PrivateKey { get; init; }

        public MiniRoleMongo Role { get; init; } = null!;

        public DateTimeOffset CreatedOn { get; init; }

        public ClientSystemMongo()
        {
            this.Id = Db.NewId();
            this.PrivateKey = Db.NewId();
            this.CreatedOn = DateTimeOffset.UtcNow;
        }

        public ClientSystemMongo(
            string name,
            MiniRoleMongo role)
            : this()
        {
            this.Name = name;
            this.Role = role;
        }
    }
}
