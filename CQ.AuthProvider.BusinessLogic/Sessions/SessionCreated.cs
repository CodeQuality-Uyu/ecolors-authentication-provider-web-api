using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public sealed record class SessionCreated
    {
        public readonly Account Account;

        public readonly string Email;

        public readonly string Token;

        public SessionCreated(
            Account account,
            string email,
            string token)
        {
            Account = account;
            Email = email;
            Token = token;
        }
    }
}
