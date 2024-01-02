using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateSessionCredentialsRequest : Request<CreateSessionCredentials>
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        protected override CreateSessionCredentials InnerMap()
        {
            return new CreateSessionCredentials(Email ?? string.Empty, Password ?? string.Empty);
        }
    }
}
