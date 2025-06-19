using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Me;
public interface IMeService
{
    Task UpdatePasswordLoggedAsync(
        string oldPassword,
        string newPassword,
        AccountLogged accountLogged);
}
