using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts
{
    public sealed record class CreateAccountResponse : Response<CreateAccountResult>
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string FullName { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Token { get; init; }

        public List<string> Roles { get; init; }

        public List<string> Permissions { get; init; }

        public CreateAccountResponse(CreateAccountResult entity) : base(entity)
        {
            this.Id = entity.Id;
            this.Email = entity.Email;
            this.FullName = entity.FullName;
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
            this.Token = entity.Token;
            this.Roles = entity.Roles.Select(r => r.ToString()).ToList();
            this.Permissions = entity.Permissions.Select(p => p.ToString()).ToList();
        }
    }
}
