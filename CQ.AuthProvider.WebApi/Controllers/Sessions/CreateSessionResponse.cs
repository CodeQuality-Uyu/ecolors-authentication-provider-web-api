using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    public sealed record class CreateSessionResponse
    {
        public string AccountId { get; }

        public string Email { get; }

        public string Token { get; }

        public List<string> Roles { get; }

        public List<string> Permissions { get; }
        
        public CreateSessionResponse(SessionCreated session)
        { 
            this.AccountId = session.AccountId;
            this.Email = session.Email;
            this.Token = session.Token;
            this.Roles = session.Roles;
            this.Permissions = session.Permissions;
        }
    }
}
