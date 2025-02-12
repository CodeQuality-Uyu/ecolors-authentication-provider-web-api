using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record class FakeAccountLogged
    : AccountLogged
{
    public new List<Guid> AppsIds
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

    public new List<Guid> RolesIds
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

    public Guid AppLoggedId
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

    public new List<string> PermissionsKeys
    {
        get => Roles
            .SelectMany(r => r.Permissions)
            .Select(p => p.Key).ToList();

        init
        {
            var permissions = value.ConvertAll(i => new Permission
            {
                Key = i
            });

            var role = Roles.Count == 0 ? new Role { Name = "Test" } : Roles[0];
            if (Roles.Count == 0)
            {
                Roles.Add(role);
            }
            
            role
                .Permissions
                .AddRange(permissions);
        }
    }

    public Guid TenantId
    {
        get => Tenant.Id;
        init
        {
            Tenant = new Tenant
            {
                Id = value
            };
        }
    }
}
