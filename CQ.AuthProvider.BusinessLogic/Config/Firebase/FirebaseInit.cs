using CQ.AuthProvider.BusinessLogic.Config.Firebase.Exceptions;
using CQ.AuthProvider.Utility;
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

namespace CQ.AuthProvider.BusinessLogic.Config
{
    internal static class FirebaseInit
    {
        public static void AddFirebase(this IServiceCollection services)
        {
            var credentials = BuildCredentials();
            var projectId = Environment.GetEnvironmentVariable("firebase:projectId");

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
        }

        private static GoogleCredential BuildCredentials()
        {
            var projectId = Environment.GetEnvironmentVariable("firebase:projectId");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:projectId");
            var privateKeyId = Environment.GetEnvironmentVariable("firebase:private-key-id");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:private-key-id");

            var privateKey = Environment.GetEnvironmentVariable("firebase:private-key");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:private-key");

            var clientEmail = Environment.GetEnvironmentVariable("firebase:client-email");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:client-email");

            var clientId = Environment.GetEnvironmentVariable("firebase:client-id");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:client-id");

            var authUri = Environment.GetEnvironmentVariable("firebase:auth-uri");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:auth-uri");

            var tokenUri = Environment.GetEnvironmentVariable("firebase:token-uri");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:token-uri");

            var authProvider = Environment.GetEnvironmentVariable("firebase:auth-provider");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:auth-provider");

            var clientCert = Environment.GetEnvironmentVariable("firebase:client-cert");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:client-cert");

            var universeDomain = Environment.GetEnvironmentVariable("firebase:universe-domain");
            Guard.ThrowIsNullOrEmpty(projectId, "firebase:universe-domain");


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
