using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public sealed record class Session
    {
        public string Id { get; init; }

        public string AccountId { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;

        public Session() 
        {
            this.Id = Db.NewId();
            this.Token = Db.NewId();
        }

        public Session(
            string accountId,
            string email,
            string? token = null)
            : this()
        {
            this.AccountId = accountId;
            this.Email = email;
            this.Token = token ?? this.Token;
        }
    }
}
