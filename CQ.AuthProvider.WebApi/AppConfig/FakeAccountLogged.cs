using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record class FakeAccountLogged
    : AccountLogged
{
    public new List<string> AppsIds { get; init; } = [];

    public new List<string> RolesIds { get; init; } = [];

    public string AppLoggedId { get; init; } = null!;

    public List<string> PermissionsKeys { get; init; } = [];

    public string TenantId { get; init; } = null!;

    public AccountLogged Build() => new()
    {
        Id = Id,
        Email = Email,
        FirstName = FirstName,
        LastName = LastName,
        FullName = FullName,
        ProfilePictureUrl = ProfilePictureUrl,
        Locale = Locale,
        TimeZone = TimeZone,
        Token = Token,
        Apps = AppsIds.ConvertAll(a => new App
        {
            Id = a
        }),
        AppLogged = new App
        {
            Id = AppLoggedId
        },
        Roles = RolesIds
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
        .ToList(),
        Tenant = new Tenant
        {
            Id = TenantId
        }
    };
}
