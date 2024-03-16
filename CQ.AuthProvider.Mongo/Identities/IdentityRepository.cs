using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.MongoDriver;

namespace CQ.AuthProvider.Mongo
{
    public sealed class IdentityRepository : 
        MongoDriverRepository<Identity>,
        IIdentityProviderRepository,
        IIdentityProviderHealthService
    {
        public IdentityRepository(MongoContext context) : base(context) { }

        public new async Task CreateAsync(Identity identity)
        {
            await base.CreateAsync(identity).ConfigureAwait(false);
        }

        public string GetName()
        {
            var databaseInfo = base._mongoContext.GetDatabaseInfo();

            return $"{databaseInfo.Provider}-{databaseInfo.Name}";
        }

        public bool Ping()
        {
            return base._mongoContext.Ping();
        }

        public async Task UpdatePasswordAsync(string identityId, string newPassword)
        {
            await base.UpdateByIdAsync(identityId, new { Password = newPassword }).ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await base.DeleteAsync(i => i.Id == id).ConfigureAwait(false);
        }
    }
}
