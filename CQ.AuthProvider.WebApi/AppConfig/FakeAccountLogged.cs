using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record class FakeAccountLogged
    : AccountLogged
{
    public new List<string> AppsIds
    {
        get => Apps.ConvertAll(i => i.Id);

        init
        {
            Apps = value.ConvertAll(a => new App
            {
                Id = a
            });
        }
    }

    public new List<string> RolesIds
    {
        get => Roles.ConvertAll(i => i.Id);

        init
        {
            Roles = value.ConvertAll(i =>
            new Role
            {
                Id = i,
            });

        }
    }

    public string AppLoggedId
    {
        get => AppLogged.Id;

        init
        {
            AppLogged = new App
            {
                Id = value
            };
        }
    }

    public List<string> PermissionsKeys
    {
        get => Roles
            .SelectMany(r => r.Permissions)
            .Select(p => p.Key).ToList();

        init
        {
            var role = Roles[0];
            role
                .Permissions
                .AddRange(value.ConvertAll(i => new Permission
                {
                    Key = i
                }));
        }
    }

    public string TenantId
    {
        get => TenantValue.Id;
        init
        {
            Tenant = new Tenant
            {
                Id = value
            };
        }
    }
}
