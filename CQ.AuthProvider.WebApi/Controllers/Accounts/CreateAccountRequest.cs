using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateAccountRequest : Request<CreateAccountArgs>
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? ProfilePictureUrl { get; init; }

        public string? Role { get; init; }

        protected override CreateAccountArgs InnerMap()
        {
            return new CreateAccountArgs(
               Email ?? string.Empty,
               Password ?? string.Empty,
               FirstName ?? string.Empty,
                LastName ?? string.Empty,
                Role,
                ProfilePictureUrl);
        }
    }
}
