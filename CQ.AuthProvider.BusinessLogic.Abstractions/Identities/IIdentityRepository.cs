namespace CQ.AuthProvider.BusinessLogic.Identities;

public interface IIdentityRepository
{
    Task CreateAndSaveAsync(Identity identity);

    Task UpdatePasswordByIdAsync(
        string identityId,
        string newPassword);

    Task DeleteByIdAsync(string id);

    Task<Identity> GetByCredentialsAsync(
        string email,
        string password);
}
