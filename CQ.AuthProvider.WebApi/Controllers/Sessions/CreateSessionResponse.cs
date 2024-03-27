using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions
{
    public sealed record class CreateSessionResponse
    {
        public string AccountId { get; init; }

        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string FulLName { get; init; }

        public string Token { get; init; }

        public List<string> Roles { get; init; }

        public List<string> Permissions { get; init; }

        public CreateSessionResponse(SessionCreated session)
        { 
            this.AccountId = session.AccountId;
            this.Email = session.Email;
            this.FulLName = session.FullName;
            this.FirstName = session.FirstName;
            this.LastName = session.LastName;
            this.Token = session.Token;
            this.Roles = session.Roles.Select(r => r.ToString()).ToList();
            this.Permissions = session.Permissions.Select(p => p.ToString()).ToList();
        }
    }
}
