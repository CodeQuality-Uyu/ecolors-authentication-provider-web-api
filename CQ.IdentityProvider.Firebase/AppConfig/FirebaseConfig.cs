using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.IdentityProvider.Firebase.AppConfig
{
    public static class FirebaseConfig
    {
        public static IServiceCollection AddFirebaseRepositories(
            this IServiceCollection services,
            IConfiguration configuration,
            IdentityFirebaseOptions identityFirebase)
        {
            var identityOptions = configuration
                .GetSection(ConfigOptions.Identity)
                .Get<IdentityFirebaseOptions>();

            services
                .AddFirebase(identityFirebase)
                .AddSingleton<IdentityFirebaseOptions>(identityOptions)
                .AddScoped<IIdentityProviderRepository, IdentityService>()
                .AddScoped<IIdentityProviderHealthService, IdentityService>()
                .AddScoped<HttpClientAdapter>((serviceProvider) =>
                {
                    var apiUrl = identityFirebase.RefererApiUrl;
                    var baseUrl = identityFirebase.ApiUrl;

                    var baseHeaders = new List<Header>
                {
                    new ("Referer", apiUrl)
                };

                    return new HttpClientAdapter(baseUrl, baseHeaders);
                })
                .AddScoped<ISessionService, SessionService>()
                .AddScoped<ISessionInternalService, SessionService>();


            return services;
        }
    }
}
