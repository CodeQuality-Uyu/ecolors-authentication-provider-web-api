using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.Mongo.Accounts;
using CQ.AuthProvider.DataAccess.Mongo.Permissions;
using CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;
using CQ.AuthProvider.DataAccess.Mongo.Roles;
using CQ.AuthProvider.DataAccess.Mongo.Sessions;
using CQ.Extensions.ServiceCollection;
using CQ.UnitOfWork.MongoDriver.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CQ.AuthProvider.DataAccess.Mongo.AppConfig;

public static class MongoRepositoriesConfig
{
    public static IServiceCollection AddMongoRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Auth");

        services
            .AddContext<AuthDbContext>(
            new MongoClient(connectionString),
            LifeTime.Scoped)
            .AddAbstractionRepository<AccountMongo, IAccountRepository, AccountRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<PermissionMongo, IPermissionRepository, PermissionRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<RoleMongo, IRoleRepository, RoleRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<SessionMongo, ISessionRepository, SessionRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<ResetPasswordMongo, IResetPasswordRepository, ResetPasswordRepository>(LifeTime.Scoped)
            ;

        return services;
    }
}
