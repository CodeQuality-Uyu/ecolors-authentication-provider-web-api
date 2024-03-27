using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public record class IdentityOptions
    {
        public const string Identity = "Identity";

        public IdentityType Type { get; init; } = IdentityType.Database;
    }

    public enum IdentityType
    {
        Database,
        Firebase
    }
}
