namespace CQ.AuthProvider.BusinessLogic.Identities;

public interface IIdentityRepository
{
    Task CreateAndSaveAsync(Identity identity);

    Task UpdatePasswordByIdAsync(
        Guid identityId,
        string newPassword);

    Task DeleteByIdAsync(Guid id);

    Task<Identity> GetByCredentialsAsync(
        string email,
        string password);
}
