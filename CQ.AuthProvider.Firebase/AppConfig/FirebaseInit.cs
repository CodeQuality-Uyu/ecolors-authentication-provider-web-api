using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.Firebase.AppConfig.Exceptions;
using CQ.Utility;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Firebase.AppConfig
{
    internal static class FirebaseInit
    {
        public static IServiceCollection AddFirebase(
            this IServiceCollection services,
            IdentityFirebaseOptions identityFirebase)
        {
            var credentials = BuildCredentials(identityFirebase);

            var playerFinderApp = FirebaseApp.Create(new AppOptions
            {
                Credential = credentials
            });

            if (playerFinderApp == null)
            {
                throw new FirebaseInitAppException(identityFirebase.ProjectId);
            }

            services.AddScoped((serviceProvider) =>
            {
                var firebaseAuth = FirebaseAuth.GetAuth(playerFinderApp);

                if (firebaseAuth == null)
                {
                    throw new FirebaseInitAuthException(identityFirebase.ProjectId);
                }

                return firebaseAuth;
            });

            return services;
        }

        private static GoogleCredential BuildCredentials(IdentityFirebaseOptions identityFirebase)
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
}
