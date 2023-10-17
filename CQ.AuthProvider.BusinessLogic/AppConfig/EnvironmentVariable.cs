using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal record EnvironmentVariable
    {
        public static EnvironmentVariable ApiUrl = new("api-url");

        public static EnvironmentVariable FirebaseApiKey = new("firebase:api-key");
        
        public static EnvironmentVariable AuthType = new("auth-type");

        public static EnvironmentVariable FirebaseApiUrl = new("firebase:api-url");

        public string Value { get; }

        public EnvironmentVariable(string value)
        {
            Value = value;
        }
    }
}
