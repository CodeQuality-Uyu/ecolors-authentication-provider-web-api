using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed class IdentityDatabaseOptions : IdentityOptions
    {
        public string Engine { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ConnectionString { get; set; } = string.Empty;
    }
}
