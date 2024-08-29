namespace CQ.IdentityProvider.Firebase.AppConfig;

internal record class IdentityFirebaseSection
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
