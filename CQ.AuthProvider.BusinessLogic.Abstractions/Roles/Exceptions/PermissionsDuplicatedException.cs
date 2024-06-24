using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions
{
    public sealed class PermissionsDuplicatedException(List<PermissionKey> keys)
        : Exception
    {
        public List<PermissionKey> Keys { get; init; } = keys;
    }
}
