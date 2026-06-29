namespace CQ.AuthProvider.BusinessLogic.Permissions;

public sealed record UpdatePermissionArgs(
    string Name,
    string Description,
    string Key,
    bool IsPublic,
    Guid AppId);

public sealed record UpdatePermissionByIdArgs(
    Guid Id,
    string Name,
    string Description,
    string Key,
    bool IsPublic,
    Guid AppId);

public sealed record UpdateBulkPermissionArgs(List<UpdatePermissionByIdArgs> Permissions);
