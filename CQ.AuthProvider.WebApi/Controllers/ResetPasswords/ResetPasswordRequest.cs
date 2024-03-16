using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.ResetPasswords
{
    public sealed record class ResetPasswordRequest : Request<string>
    {
        public string? Email { get; init; }

        protected override string InnerMap()
        {
            var email = Guard.IsNotNullOrEmpty(this.Email) ? Guard.Encode(this.Email.Trim()) : null;
            Guard.ThrowIsNullOrEmpty(this.Email, nameof(this.Email));

            return email;
        }
    }
}
