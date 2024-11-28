using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Tenants;
namespace CQ.AuthProvider.BusinessLogic.Roles;

public sealed record class Role()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public List<Permission> Permissions { get; init; } = [];

    public App App { get; init; } = null!;

    public Guid AppId => App.Id;

    public Tenant Tenant { get; init; } = null!;

    public bool IsDefault { get; init; }
}
