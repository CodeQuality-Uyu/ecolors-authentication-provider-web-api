using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

public sealed record class AddPermissionRequest : Request<AddPermissionArgs>
{
    public List<string>? PermissionsKeys { get; init; }

    protected override AddPermissionArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(PermissionsKeys, nameof(PermissionsKeys));

        return new AddPermissionArgs(PermissionsKeys!);
    }
}
