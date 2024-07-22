using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record class FakeAccountLogged
    : AccountLogged
{
    public List<string> AppsIds { get; init; } = [];

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
        Roles = PermissionsKeys.Count == 0
            ? []
            : [
            new Role
            {
                Permissions = PermissionsKeys.ConvertAll(p => new Permission
                {
                    Key = new PermissionKey(p)
                })
            }],
        Tenant = new Tenant
        {
            Id = TenantId
        }
    };
}
