using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.Firebase.AppConfig
{
    public static class FirebaseDependencyInjection
    {
        public static IServiceCollection AddFirebaseServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IdentityFirebaseOptions identityFirebase)
        {
            var identityOptions = configuration
                .GetSection(IdentityOptions.Identity)
                .Get<IdentityFirebaseOptions>()!;

            services
                .AddFirebase(identityFirebase)
                .AddScoped<IdentityFirebaseOptions>((provider) => identityOptions)
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
