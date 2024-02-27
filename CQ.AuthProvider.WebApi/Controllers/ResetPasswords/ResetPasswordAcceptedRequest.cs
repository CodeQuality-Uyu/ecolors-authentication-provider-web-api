using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.ResetPasswords
{
    public sealed record class ResetPasswordAcceptedRequest : Request<ResetPasswordApplicationAccepted>
    {
        public string? NewPassword { get; init; }

        public int? Code { get; init; }

        protected override ResetPasswordApplicationAccepted InnerMap()
        {
            Guard.ThrowIsNullOrEmpty(this.NewPassword, nameof(this.NewPassword));
            Guard.ThrowIsNullOrEmpty($"{this.Code}", nameof(this.Code));

            return new ResetPasswordApplicationAccepted(NewPassword, $"{this.Code}");
        }
    }
}
