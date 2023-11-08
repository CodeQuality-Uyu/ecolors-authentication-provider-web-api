using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public sealed record class CreateAuthRequest : Request<CreateAuth>
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Role { get; init; }

        protected override CreateAuth InnerMap()
        {
            return new CreateAuth(
               Email ?? string.Empty,
               Password ?? string.Empty,
               FirstName ?? string.Empty,
                LastName ?? string.Empty,
                Role ?? string.Empty);
        }
    }
}
