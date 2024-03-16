using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateAccountRequest : Request<CreateAccount>
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Role { get; init; }

        protected override CreateAccount InnerMap()
        {
            return new CreateAccount(
               Email ?? string.Empty,
               Password ?? string.Empty,
               FirstName ?? string.Empty,
                LastName ?? string.Empty,
                Role ?? string.Empty);
        }
    }
}
