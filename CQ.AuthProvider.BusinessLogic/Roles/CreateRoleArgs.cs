
namespace CQ.AuthProvider.BusinessLogic.Roles;

public sealed record CreateRoleArgs(
    string Name,
    string Description,
    List<string> PermissionKeys,
    bool IsPublic,
    bool IsDefault,
    string? AppId);
