using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed record class EnvironmentVariable
    {
        public static readonly FirebaseVariable Firebase = new();

        public static readonly MongoVariable MongoIdentityProvider = new("identity-provider");

        public static readonly MongoVariable MongoAuth = new("auth");

        public string Value { get; }

        internal EnvironmentVariable(string value)
        {
            Value = value;
        }
    }

    public sealed record class FirebaseVariable
    {
        public readonly EnvironmentVariable ProjectName = new("firebase-projectName");

        public readonly EnvironmentVariable ProjectId = new("firebase-projectId");

        public readonly EnvironmentVariable PrivateKeyId = new("firebase-private-key-id");

        public readonly EnvironmentVariable PrivateKey = new("firebase-private-key");

        public readonly EnvironmentVariable ClientEmail = new("firebase-client-email");

        public readonly EnvironmentVariable ClientId = new("firebase-client-id");

        public readonly EnvironmentVariable AuthUri = new("firebase-auth-uri");

        public readonly EnvironmentVariable TokenUri = new("firebase-token-uri");

        public readonly EnvironmentVariable AuthProvider = new("firebase-auth-provider");

        public readonly EnvironmentVariable ClientCert = new("firebase-client-cert");

        public readonly EnvironmentVariable UniverseDomain = new("firebase-universe-domain");

        public readonly EnvironmentVariable ApiKey = new("firebase-api-key");

        public readonly EnvironmentVariable ApiUrl = new("firebase-api-url");

        public readonly EnvironmentVariable RefererApiUrl = new("firebase-referer-api-url");
    }

    public sealed record class MongoVariable
    {
        public readonly EnvironmentVariable ConnectionString;

        public readonly EnvironmentVariable DataBaseName;

        public MongoVariable(string databaseType)
        {
            ConnectionString = new($"mongo-{databaseType}-connection-string");
            DataBaseName = new($"mongo-{databaseType}-database-name");
        }
    }
}
