using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class MiniRole
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string Key { get; init; }
    }
}
