using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.EfCore.Core;

namespace CQ.IdentityProvider.EfCore.Identities;

public sealed class IdentityRepository(
    EfCoreContext context)
    : EfCoreRepository<Identity>(context),
    IIdentityRepository
{
    public async Task DeleteByIdAsync(string id)
    {
        await DeleteAsync(i => i.Id == id).ConfigureAwait(false);
    }

    public async Task<Identity> GetByCredentialsAsync(string email, string password)
    {
        var identity = await GetAsync(i => i.Email == email && i.Password == password).ConfigureAwait(false);

        return identity;
    }

    public async Task UpdatePasswordAsync(string id, string newPassword)
    {
        var identity = await GetAsync(i => i.Id == id).ConfigureAwait(false);

        identity.Password = newPassword;

        await UpdateAsync(identity).ConfigureAwait(false);
    }

    async Task IIdentityRepository.CreateAsync(Identity identity)
    {
        await CreateAsync(identity).ConfigureAwait(false);
    }
}
