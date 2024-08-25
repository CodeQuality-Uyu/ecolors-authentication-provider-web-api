
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.IdentityProvider.Mongo.Identities;

internal sealed class IdentityRepository(IdentityDbContext context)
    : MongoDriverRepository<Identity>(context),
    IIdentityRepository
{
    public new async Task CreateAsync(Identity identity)
    {
        await base.CreateAsync(identity).ConfigureAwait(false);
    }

    public async Task UpdatePasswordByIdAsync(string identityId, string newPassword)
    {
        await base.UpdateByIdAsync(identityId, new { Password = newPassword }).ConfigureAwait(false);
    }

    public async Task DeleteByIdAsync(string id)
    {
        await base.DeleteAsync(i => i.Id == id).ConfigureAwait(false);
    }

    public async Task<Identity> GetByCredentialsAsync(string email, string password)
    {
        var identity = await GetAsync(i => i.Email == email && i.Password == password).ConfigureAwait(false);

        return identity;
    }
}
