
namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public sealed record class ConnectionStrings
    {
        public const string Auth = "Auth";
        public const string Identity = "Identity";
    }

    public sealed record class ConfigOptions
    {
        public const string Auth = "Auth";
        public const string Identity = "Identity";
    }

    public sealed record class AuthOptions
    {
        public DatabaseEngine Engine { get; init; } = DatabaseEngine.Sql;

        public string DatabaseName { get; init; } = null!;

        public string PrivateKey { get; init; } = null!;
    }

    public record class IdentityOptions
    {
        public IdentityType Type { get; init; } = IdentityType.Database;
    }

    public enum IdentityType
    {
        Database,
        Firebase
    }

    public sealed record class IdentityDatabaseOptions : IdentityOptions
    {
        public DatabaseEngine Engine { get; init; } = DatabaseEngine.Sql;

        public string DatabaseName { get; init; } = null!;
    }

    public enum DatabaseEngine
    {
        Sql,
        Mongo
    }

    public sealed record class IdentityFirebaseOptions : IdentityOptions
    {
        public string ProjectName { get; init; } = null!;

        public string ProjectId { get; init; } = null!;

        public string PrivateKeyId { get; init; } = null!;

        public string PrivateKey { get; init; } = null!;

        public string ClientEmail { get; init; } = null!;

        public string ClientId { get; init; } = null!;

        public string AuthUri { get; init; } = null!;

        public string TokenUri { get; init; } = null!;

        public string AuthProvider { get; init; } = null!;

        public string ClientCert { get; init; } = null!;

        public string UniverseDomain { get; init; } = null!;

        public string ApiKey { get; init; } = null!;

        public string ApiUrl { get; init; } = null!;

        public string RefererApiUrl { get; init; } = null!;
    }
}
