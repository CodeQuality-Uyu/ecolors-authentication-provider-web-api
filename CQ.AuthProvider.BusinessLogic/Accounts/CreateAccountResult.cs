using CQ.AuthProvider.BusinessLogic.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public record class CreateAccountResult(
        string Id,
        string Email,
        string FullName,
        string FirstName,
        string LastName,
        string Token,
        List<RoleKey> Roles,
        List<PermissionKey> Permissions);
}
