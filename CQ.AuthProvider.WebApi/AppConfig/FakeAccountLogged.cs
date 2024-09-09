using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record class FakeAccountLogged
    : AccountLogged
{
    public new List<string> AppsIds { get; init; } = [];

    public new List<App> Apps => AppsIds.ConvertAll(a => new App
    {
        Id = a
    });

    public new List<string> RolesIds { get; init; } = [];

    public new List<Role> Roles => RolesIds
        .Select((i, index) => new Role
        {
            Id = i,
            Permissions = index == 0
            ? PermissionsKeys.ConvertAll(p => new Permission
            {
                Key = p
            })
            : []
        })
        .ToList();

    public string AppLoggedId { get; init; } = null!;

    public new App AppLogged => new ()
    {
        Id = AppLoggedId
    };

    public List<string> PermissionsKeys { get; init; } = [];

    public string TenantId { get; init; } = null!;

    public new Tenant Tenant => new ()
    {
        Id = TenantId
    };
}
