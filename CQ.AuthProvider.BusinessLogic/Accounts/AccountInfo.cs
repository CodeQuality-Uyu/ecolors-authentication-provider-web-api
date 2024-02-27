using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountInfo(string Id, List<string> Roles, List<string> Permissions);
}
