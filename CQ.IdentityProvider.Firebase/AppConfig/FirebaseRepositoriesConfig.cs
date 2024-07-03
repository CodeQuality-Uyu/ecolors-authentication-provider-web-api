using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CQ.Extensions.Configuration;
using CQ.IdentityProvider.Firebase.AppConfig.Exceptions;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.IdentityProvider.Firebase.Identities;
using CQ.Extensions.ServiceCollection;

namespace CQ.IdentityProvider.Firebase.AppConfig;

public static class FirebaseRepositoriesConfig
{
    public static IServiceCollection AddFirebaseRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var identitySection = configuration.GetSection<IdentityFirebaseSection>("Identity");

        services
            .AddFirebase(identitySection)
            .AddSingleton<IdentityFirebaseSection>(identitySection)
            .AddScoped<IIdentityRepository, IdentityRepository>()
            .AddScoped<IIdentityProviderHealthService, IdentityRepository>()
            .AddScoped<HttpClientAdapter>((serviceProvider) =>
            {
                var apiUrl = identitySection.RefererApiUrl;
                var baseUrl = identitySection.ApiUrl;

                var baseHeaders = new List<Header>
            {
                new ("Referer", apiUrl)
            };

                return new HttpClientAdapter(baseUrl, baseHeaders);
            })
            ;

        return services;
    }

    private static IServiceCollection AddFirebase(
            this IServiceCollection services,
            IdentityFirebaseSection identityFirebase)
    {
        var credentials = BuildCredentials(identityFirebase);

        var playerFinderApp = FirebaseApp.Create(new AppOptions
        {
            Credential = credentials
        });

        if (Guard.IsNull(playerFinderApp))
        {
            throw new FirebaseInitAppException(identityFirebase.ProjectId);
        }

        var firebaseAuth = FirebaseAuth.GetAuth(playerFinderApp);

        if (Guard.IsNull(firebaseAuth))
        {
            throw new FirebaseInitAuthException(identityFirebase.ProjectId);
        }

        services.AddService<FirebaseAuth>(firebaseAuth, LifeTime.Scoped);

        return services;
    }

    private static GoogleCredential BuildCredentials(IdentityFirebaseSection identityFirebase)
    {
        var credentials = GoogleCredential.FromJson($@"
                    {{
                        ""type"":""{JsonCredentialParameters.ServiceAccountCredentialType}"",
                        ""project_id"":""{identityFirebase.ProjectId}"",                        
                        ""private_key_id"":""{identityFirebase.PrivateKeyId}"",
                        ""private_key"":""{identityFirebase.PrivateKey}"",
                        ""client_id"":""{identityFirebase.ClientId}"",
                        ""client_email"":""{identityFirebase.ClientEmail}"",
                        ""auth_uri"":""{identityFirebase.AuthUri}"",
                        ""token_uri"":""{identityFirebase.TokenUri}"",
                        ""auth_provider_x509_cert_url"":""{identityFirebase.AuthProvider}"",
                        ""client_x509_cert_url"":""{identityFirebase.ClientCert}"",
                        ""universe_domain"":""{identityFirebase.UniverseDomain}""
                    }}");

        return credentials;
    }
}
