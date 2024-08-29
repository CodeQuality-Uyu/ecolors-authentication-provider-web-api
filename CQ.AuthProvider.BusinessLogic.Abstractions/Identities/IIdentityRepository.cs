using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

public interface IIdentityRepository
{
    Task CreateAsync(Identity identity);

    Task UpdatePasswordByIdAsync(
        string identityId,
        string newPassword);

    Task DeleteByIdAsync(string id);

    Task<Identity> GetByCredentialsAsync(
        string email,
        string password);
}
