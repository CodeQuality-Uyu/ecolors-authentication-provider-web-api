using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions
{
    public sealed class PermissionsDuplicatedException(List<string> keys)
        : Exception
    {
        public List<string> Keys { get; init; } = keys;
    }
}
