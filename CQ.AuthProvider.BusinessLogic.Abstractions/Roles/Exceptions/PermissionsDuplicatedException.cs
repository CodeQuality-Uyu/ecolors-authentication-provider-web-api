namespace CQ.AuthProvider.BusinessLogic.Roles.Exceptions
{
    public sealed class PermissionsDuplicatedException(List<string> keys)
        : Exception
    {
        public List<string> Keys { get; init; } = keys;
    }
}
