namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public sealed record class SessionCreated
    {
        public readonly string AccountId;

        public readonly string Email;

        public readonly string Token;

        public readonly List<string> Roles;

        public readonly List<string> Permissions;

        public SessionCreated(
            string accountId,
            string email,
            string token,
            List<string> roles,
            List<string> permissions)
        {
            this.AccountId = accountId;
            this.Email = email;
            this.Token = token;
            this.Roles = roles;
            this.Permissions = permissions;
        }
    }
}
