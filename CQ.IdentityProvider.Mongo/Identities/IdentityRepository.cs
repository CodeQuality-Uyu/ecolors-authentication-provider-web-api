using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.MongoDriver;

namespace CQ.IdentityProvider.Mongo
{
    public sealed class IdentityRepository : 
        MongoDriverRepository<Identity>,
        IIdentityProviderRepository
    {
        public IdentityRepository(MongoContext context) : base(context) { }

        public new async Task CreateAsync(Identity identity)
        {
            await base.CreateAsync(identity).ConfigureAwait(false);
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
