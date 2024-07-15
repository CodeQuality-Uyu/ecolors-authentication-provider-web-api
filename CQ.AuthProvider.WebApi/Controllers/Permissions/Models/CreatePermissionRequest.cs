using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

public sealed record class CreatePermissionRequest : Request<CreatePermissionArgs>
{
    public string? Name { get; init; }

    public string? Description { get; init; }

    public string? Key { get; init; }

    public bool? IsPublic { get; init; }

    public string? AppId { get; init; }

    protected override CreatePermissionArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Name, nameof(Name));
        Guard.ThrowIsNullOrEmpty(Description, nameof(Description));
        Guard.ThrowIsNullOrEmpty(Key, nameof(Key));
        Guard.ThrowIsNull(IsPublic, nameof(IsPublic));

        return new CreatePermissionArgs(
            Name!,
            Description!,
            Key!,
            IsPublic!.Value,
            AppId);
    }
}
