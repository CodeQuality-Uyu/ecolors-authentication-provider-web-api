using CQ.AuthProvider.BusinessLogic.AppConfig.Firebase.Exceptions;
using CQ.Utility;
using dotenv.net.Utilities;
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

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    internal static class FirebaseInit
    {
        public static IServiceCollection AddFirebase(this IServiceCollection services, ISettingsService settingsService)
        {
            var credentials = BuildCredentials(settingsService);
            var projectId = settingsService.GetValue(EnvironmentVariable.Firebase.ProjectId);

            var playerFinderApp = FirebaseApp.Create(new AppOptions
            {
                Credential = credentials
            });

            if (playerFinderApp == null)
            {
                throw new FirebaseInitAppException(projectId);
            }

            services.AddTransient((serviceProvider) =>
            {
                var firebaseAuth = FirebaseAuth.GetAuth(playerFinderApp);

                if (firebaseAuth == null)
                {
                    throw new FirebaseInitAuthException(projectId);
                }

                return firebaseAuth;
            });

            return services;
        }

        private static GoogleCredential BuildCredentials(ISettingsService settingsService)
        {
            var firebaseConfig = EnvironmentVariable.Firebase;

            var projectId = settingsService.GetValue(firebaseConfig.ProjectId);

            var privateKeyId = settingsService.GetValue(firebaseConfig.PrivateKeyId);

            var privateKey = settingsService.GetValue(firebaseConfig.PrivateKey);

            var clientEmail = settingsService.GetValue(firebaseConfig.ClientEmail);

            var clientId = settingsService.GetValue(firebaseConfig.ClientId);

            var authUri = settingsService.GetValue(firebaseConfig.AuthUri);

            var tokenUri = settingsService.GetValue(firebaseConfig.TokenUri);

            var authProvider = settingsService.GetValue(firebaseConfig.AuthProvider);

            var clientCert = settingsService.GetValue(firebaseConfig.ClientCert);

            var universeDomain = settingsService.GetValue(firebaseConfig.UniverseDomain);


            var credentials = GoogleCredential.FromJson($@"
                    {{
                        ""type"":""{JsonCredentialParameters.ServiceAccountCredentialType}"",
                        ""project_id"":""{projectId}"",                        
                        ""private_key_id"":""{privateKeyId}"",
                        ""private_key"":""{privateKey}"",
                        ""client_id"":""{clientId}"",
                        ""client_email"":""{clientEmail}"",
                        ""auth_uri"":""{authUri}"",
                        ""token_uri"":""{tokenUri}"",
                        ""auth_provider_x509_cert_url"":""{authProvider}"",
                        ""client_x509_cert_url"":""{clientCert}"",
                        ""universe_domain"":""{universeDomain}""
                    }}");

            return credentials;
        }
    }
}
