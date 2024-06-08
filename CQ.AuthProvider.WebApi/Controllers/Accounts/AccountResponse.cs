using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts
{
    public record class AccountResponse : Response<Account>
    {
        public string Id { get; init; }

        public string? ProfilePictureUrl { get; init; }

        public string FullName { get; init; }
        
        public string FirstName { get; init; }
        
        public string LastName { get; init; }

        public string Email { get; init; }

        public List<string> Roles { get; init; }

        public List<string> Permissions { get; init; }

        public AccountResponse(Account entity) : base(entity)
        {
            Id = entity.Id;
            FullName = entity.FullName;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            Roles = entity.Roles.Select(r => r.ToString()).ToList();
            Permissions = entity.Permissions.Select(p => p.ToString()).ToList();
            ProfilePictureUrl = entity.ProfilePictureUrl;
        }
    }
}
