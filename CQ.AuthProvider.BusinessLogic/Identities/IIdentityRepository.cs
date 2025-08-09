namespace CQ.AuthProvider.BusinessLogic.Identities;

public interface IIdentityRepository
{
    Task CreateAndSaveAsync(Identity identity, bool passwordIsHash = false);

    Task UpdatePasswordByIdAsync(
        Guid id,
        string oldPassword,
        string newPassword);

    Task DeleteByIdAsync(Guid id);

    Task<Identity> GetByCredentialsAsync(
        string email,
        string password);
}
