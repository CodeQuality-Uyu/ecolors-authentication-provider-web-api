namespace CQ.AuthProvider.BusinessLogic.Permissions;

public sealed record CreatePermissionArgs(
    string Name,
    string Description,
    string Key,
    bool IsPublic,
    Guid? AppId);
