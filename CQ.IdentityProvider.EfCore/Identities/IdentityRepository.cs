using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.AspNetCore.Identity;

namespace CQ.IdentityProvider.EfCore.Identities;

public sealed class IdentityRepository(
    PasswordHasher<string> passwordHasher,
    IdentityDbContext context)
    : EfCoreRepository<Identity>(context),
    IIdentityRepository
{
    public async Task DeleteByIdAsync(Guid id)
    {
        await DeleteAndSaveAsync(i => i.Id == id).ConfigureAwait(false);
    }

    public async Task<Identity> GetByCredentialsAsync(
        string email,
        string password)
    {
        var identity = await GetAsync(i => i.Email == email).ConfigureAwait(false);

        var result = passwordHasher.VerifyHashedPassword(email, identity.Password, password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new SpecificResourceNotFoundException<Identity>("condition", string.Empty);
        }

        return identity;
    }

    public async Task UpdatePasswordByIdAsync(
        Guid id,
        string newPassword)
    {
        var passwordHashed = passwordHasher.HashPassword(id.ToString(), newPassword);

        await UpdateAndSaveByIdAsync(id, new { Password = passwordHashed }).ConfigureAwait(false);
    }

    async Task IIdentityRepository.CreateAndSaveAsync(Identity identity)
    {
        identity.Password = passwordHasher.HashPassword(identity.Id.ToString(), identity.Password);

        await CreateAndSaveAsync(identity).ConfigureAwait(false);
    }
}
