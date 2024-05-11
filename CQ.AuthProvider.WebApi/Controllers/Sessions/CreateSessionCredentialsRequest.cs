using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    public sealed record class CreateSessionCredentialsRequest : Request<CreateSessionCredentialsArgs>
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        public string? ListenerServer {  get; init; }

        protected override CreateSessionCredentialsArgs InnerMap()
        {
            Guard.ThrowIsNullOrEmpty(Email, nameof(Email));
            Guard.ThrowIsNullOrEmpty(Password, nameof(Password));

            return new CreateSessionCredentialsArgs(Email, Password, ListenerServer);
        }
    }
}
