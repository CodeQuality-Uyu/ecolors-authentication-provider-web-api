using CQ.AuthProvider.BusinessLogic.Apps;
using System.Security.Principal;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

public record class AccountLogged()
    : Account,
    IPrincipal
{
    public string Token { get; init; } = null!;

    public List<Guid> AppsIds => Apps.ConvertAll(a => a.Id);

    public List<Guid> RolesIds => Roles.ConvertAll(r => r.Id);

    public App AppLogged { get; init; } = null!;

    public virtual IIdentity? Identity => null;

    public AccountLogged(
        Account account,
        string token,
        App appLogged)
        : this()
    {
        Id = account.Id;
        Email = account.Email;
        FirstName = account.FirstName;
        LastName = account.LastName;
        FullName = account.FullName;
        ProfilePictureId = account.ProfilePictureId;
        Locale = account.Locale;
        TimeZone = account.TimeZone;
        Roles = account.Roles;
        Tenant = account.TenantValue;
        Token = token;
        AppLogged = appLogged;
    }

    public bool IsInRole(string role)
    {
        var isRole = Roles.Exists(r => r.Name == role);

        return isRole || HasPermission(role);
    }
}
