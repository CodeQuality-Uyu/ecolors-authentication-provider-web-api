using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQ.AuthProvider.WebApi.Controllers
{
    public class CreateAuthRequest : Request<CreateAuth>
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        protected override CreateAuth InnerMap()
        {
            return new CreateAuth(
               Email ?? string.Empty,
               Password ?? string.Empty,
               FirstName ?? string.Empty,
                LastName ?? string.Empty);
        }
    }
}
