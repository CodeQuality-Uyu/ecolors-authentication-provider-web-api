namespace CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

public interface IIdentityRepository
{
    Task CreateAsync(Identity identity);

    Task UpdatePasswordAsync(string identityId, string newPassword);

    Task DeleteByIdAsync(string id);
}
