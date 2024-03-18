using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed record class IdentityDatabaseOptions : IdentityOptions
    {
        public DatabaseEngine Engine { get; set; } = DatabaseEngine.SQL;

        public string Name { get; set; } = string.Empty;

        public string ConnectionString { get; set; } = string.Empty;
    }

    public enum DatabaseEngine
    {
        SQL,
        MONGO
    }
}
