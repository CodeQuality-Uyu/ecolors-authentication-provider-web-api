
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    public sealed record class ResetPasswordApplication
    {
        public const int TOLERANCE_IN_MINUTES = 15;

        public string Id { get; init; } = null!;

        public MiniAccount Account { get; init; } = null!;

        public string Code { get; init; } = null!;

        public DateTimeOffset CreatedAt { get; init; }

        public ResetPasswordApplication()
        {
            this.Id = Db.NewId();
            this.Code = NewCode();
            this.CreatedAt = DateTimeOffset.Now;
        }

        public ResetPasswordApplication(MiniAccount account) 
            : this()
        {
            this.Account = account;
        }

        public bool HasExpired()
        {
            return DateTimeOffset.UtcNow > this.CreatedAt.AddMinutes(TOLERANCE_IN_MINUTES);
        }

        public static string NewCode()
        {
            return new Random().Next(1111, 9999).ToString();
        }
    }

    public sealed record class MiniAccount
    {
        public string Id { get; init; } = null!;

        public string Email { get; init; } = null!;

        public MiniAccount()
        {
        }

        public MiniAccount(string id, string email)
        {
            this.Id = id;
            this.Email = email;
        }
    }
}
