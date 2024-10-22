namespace CQ.AuthProvider.BusinessLogic.Permissions;

public sealed record CreateBulkPermissionArgs(List<CreatePermissionArgs> Permissions);
