using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.EfCore.Identities;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.EfCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.EfCore.AppConfig
{
    public static class EfCoreDependencyInjection
    {
        public static IServiceCollection AddEfCoreServices(
            this IServiceCollection services,
            string name,
            string connectionString)
        {
            services
                .AddEfCoreContext<IdentityProviderEfCoreContext>(
                new EfCoreConfig(
                    new DatabaseConfig(connectionString, name),
                    useDefaultQueryLogger: true,
                     @default: false),
                LifeTime.Scoped,
                LifeTime.Scoped)
                .AddRepository<Session>(name, LifeTime.Scoped)
                .AddAbstractionEfCoreRepository<Identity, IIdentityProviderRepository, IdentityRepository>(name, LifeTime.Scoped)
                .AddScoped<IIdentityProviderHealthService, IdentityRepository>();

            return services;
        }
    }
}
