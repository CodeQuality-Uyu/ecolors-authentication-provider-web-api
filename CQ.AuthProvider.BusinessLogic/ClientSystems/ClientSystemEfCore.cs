using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems
{
    public sealed class ClientSystemEfCore
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public string PrivateKey { get; init; }

        public string RoleId { get; init; } = null!;

        public RoleEfCore Role { get; init; } = null!;

        public DateTimeOffset CreatedOn { get; init; }

        public ClientSystemEfCore()
        {
            this.Id = Db.NewId();
            this.PrivateKey = Db.NewId();
            this.CreatedOn = DateTimeOffset.UtcNow;
        }

        public ClientSystemEfCore(
            string name,
            string roleId)
            : this()
        {
            this.Name = name;
            this.RoleId = roleId;
        }
    }
}
