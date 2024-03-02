using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts
{
    public sealed record class AccountInfoResponse : Response<AccountInfo>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }

        public List<string> Roles { get; init; }

        public List<string> Permissions { get; init; }

        public AccountInfoResponse(AccountInfo entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            Roles = entity.Roles.Select(r => r.ToString()).ToList();
            Permissions = entity.Permissions.Select(p => p.ToString()).ToList();
        }
    }
}
