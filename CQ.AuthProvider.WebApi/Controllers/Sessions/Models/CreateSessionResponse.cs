using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions.Models
{
    public readonly struct CreateSessionResponse
    {
        public string AccountId { get; init; }

        public string? ProfilePictureUrl { get; init; }

        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string FullName { get; init; }

        public string Token { get; init; }

        public List<string> Roles { get; init; }

        public List<string> Permissions { get; init; }

        public CreateSessionResponse(SessionCreated session)
        {
            AccountId = session.Account.Id;
            Email = session.Email;
            FullName = session.Account.FullName;
            FirstName = session.Account.FirstName;
            LastName = session.Account.LastName;
            Token = session.Token;
            Roles = session.Account.Roles.Select(r => r.ToString()).ToList();
            Permissions = session.Account.Permissions.Select(p => p.ToString()).ToList();
            ProfilePictureUrl = session.Account.ProfilePictureUrl;
        }
    }
}
