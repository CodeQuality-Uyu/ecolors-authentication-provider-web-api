using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.EfCore;

namespace CQ.AuthProvider.EfCore.Identities
{
    public sealed class IdentityRepository
        : EfCoreRepository<Identity>,
        IIdentityProviderRepository,
        IIdentityProviderHealthService
    {
        public IdentityRepository(IdentityProviderEfCoreContext context)
            : base(context) 
        { 
        }

        public new async Task CreateAsync(Identity identity)
        {
            await base.CreateAsync(identity).ConfigureAwait(false);
        }

        public string GetName()
        {
            var databaseInfo = base._efCoreConnection.GetDatabaseInfo();

            return $"{databaseInfo.Provider}-{databaseInfo.Name}";
        }

        public bool Ping()
        {
            return base._efCoreConnection.Ping();
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
