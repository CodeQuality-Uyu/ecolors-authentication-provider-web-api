using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Roles.Models;

public sealed record class CreateRoleRequest : Request<CreateRoleArgs>
{
    public string? Name { get; init; }

    public string? Description { get; init; }

    public string? Key { get; init; }

    public List<string>? PermissionKeys { get; init; }

    public bool? IsPublic { get; init; }

    public bool? IsDefault { get; init; }

    protected override CreateRoleArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Name, nameof(Name));
        Guard.ThrowIsNullOrEmpty(Description, nameof(Description));
        Guard.ThrowIsNullOrEmpty(Key, nameof(Key));
        Guard.ThrowIsNullOrEmpty(PermissionKeys, nameof(PermissionKeys));
        Guard.ThrowIsNull(IsPublic, nameof(IsPublic));
        Guard.ThrowIsNull(IsDefault, nameof(IsDefault));

        return new CreateRoleArgs(
            Name!,
            Description!,
            Key!,
            PermissionKeys!,
            IsPublic!,
            IsDefault!);
    }
}
