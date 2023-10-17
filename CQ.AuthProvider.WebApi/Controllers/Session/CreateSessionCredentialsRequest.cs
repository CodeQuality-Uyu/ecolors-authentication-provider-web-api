using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    internal sealed class CreateSessionCredentialsRequest : Request<CreateSessionCredentials>
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        protected override CreateSessionCredentials InnerMap()
        {
            return new CreateSessionCredentials(Email ?? string.Empty, Password ?? string.Empty);
        }
    }
}
