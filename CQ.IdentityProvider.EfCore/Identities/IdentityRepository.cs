using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.EfCore.Core;

namespace CQ.IdentityProvider.EfCore.Identities;

public sealed class IdentityRepository(
    IdentityDbContext context)
    : EfCoreRepository<Identity>(context),
    IIdentityRepository
{
    public async Task DeleteByIdAsync(string id)
    {
        await DeleteAndSaveAsync(i => i.Id == id).ConfigureAwait(false);
    }

    public async Task<Identity> GetByCredentialsAsync(
        string email,
        string password)
    {
        var identity = await GetAsync(i => i.Email == email && i.Password == password).ConfigureAwait(false);

        return identity;
    }

    public async Task UpdatePasswordByIdAsync(
        string id,
        string newPassword)
    {
        var identity = await GetAsync(i => i.Id == id).ConfigureAwait(false);

        identity.Password = newPassword;

        await UpdateAndSaveAsync(identity).ConfigureAwait(false);
    }

    public async Task CreateAndSaveAsync(Identity identity)
    {
        await CreateAsync(identity).ConfigureAwait(false);
    }
}
