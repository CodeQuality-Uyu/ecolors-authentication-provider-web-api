using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
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
