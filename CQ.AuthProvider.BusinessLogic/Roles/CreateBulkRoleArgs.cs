namespace CQ.AuthProvider.BusinessLogic.Roles;

public sealed record CreateBulkRoleArgs(List<CreateRoleArgs> Roles);
