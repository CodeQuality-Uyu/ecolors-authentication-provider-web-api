using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed class AuthOptions
    {
        public const string AuthDatabaseNameKey = "DatabaseName";
        public const string SqlConnectionString= "AuthSql";
        public const string MongoConnectionString = "AuthMongo";

        public string AuthDatabaseName { get; init; } = null!;
    }
}
