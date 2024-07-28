using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts;

public sealed record class MiniAcountRole
{
    public List<string> Permissions { get; init; } = null!;

    /// <summary>
    /// For MongoDriver
    /// </summary>
    public MiniAcountRole()
    {
    }

    /// <summary>
    /// For new MiniRole
    /// </summary>
    /// <param name="key"></param>
    /// <param name="permissions"></param>
    public MiniAcountRole(
        List<Permission> permissions)
    {
        Permissions = permissions.ConvertAll(p => p.Key.ToString());
    }
}
