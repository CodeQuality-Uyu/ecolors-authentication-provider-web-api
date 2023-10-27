using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public record struct EnvironmentVariable
    {
        public static EnvironmentVariable ApiUrl = new("api-url");

        public static FirebaseVariable Firebase = new();

        public static MongoVariable Mongo = new();

        public static EnvironmentVariable AuthType = new("auth-type");


        public string Value { get; }

        internal EnvironmentVariable(string value)
        {
            Value = value;
        }
    }

    public record struct FirebaseVariable
    {
        public EnvironmentVariable ProjectId = new("firebase-projectId");

        public EnvironmentVariable PrivateKeyId = new("firebase-private-key-id");

        public EnvironmentVariable PrivateKey = new("firebase-private-key");

        public EnvironmentVariable ClientEmail = new("firebase-client-email");

        public EnvironmentVariable ClientId = new("firebase-client-id");

        public EnvironmentVariable AuthUri = new("firebase-auth-uri");

        public EnvironmentVariable TokenUri = new("firebase-token-uri");
        
        public EnvironmentVariable AuthProvider = new("firebase-auth-provider");
        
        public EnvironmentVariable ClientCert = new("firebase-client-cert");
        
        public EnvironmentVariable UniverseDomain = new("firebase-universe-domain");
        
        public EnvironmentVariable ApiKey = new("firebase-api-key");
        
        public EnvironmentVariable ApiUrl = new("firebase-api-url");
        
        public FirebaseVariable() { }
    }

    public record struct MongoVariable
    {
        public EnvironmentVariable ConnectionString = new("mongo-connection-string");

        public EnvironmentVariable DataBaseName = new("mongo-database-name");

        public MongoVariable() { }
    }
}
