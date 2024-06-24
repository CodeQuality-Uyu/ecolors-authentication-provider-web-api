using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords.Exceptions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;

internal abstract class ResetPasswordService(
    IResetPasswordRepository resetPasswordRepository,
    IIdentityRepository identityRepository,
    IAccountInternalService accountService) : IResetPasswordService
{
    public async Task AcceptAsync(
        string id,
        AcceptResetPasswordArgs args)
    {
        var resetPasswordOldApplication = await resetPasswordRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        if (resetPasswordOldApplication.Code != args.Code)
        {
            throw new CodesNotMatchException(
                args.Code,
                resetPasswordOldApplication.Account.Email);
        }

        await identityRepository
            .UpdatePasswordAsync(
            resetPasswordOldApplication.Account.Id,
            args.NewPassword)
            .ConfigureAwait(false);

        
        await resetPasswordRepository
            .UpdateStatusByIdAsync(
            resetPasswordOldApplication.Id,
            ResetPasswordStatus.Accepted)
            .ConfigureAwait(false);
    }

    public async Task CreateAsync(string email)
    {
        var oldResetPassword = await resetPasswordRepository
            .GetByEmailOfAccountAsync(email)
            .ConfigureAwait(false);

        var account = await accountService
            .GetByEmailAsync(email)
            .ConfigureAwait(false);

        string code = ResetPassword.NewCode();
        if (Guard.IsNull(oldResetPassword))
        {
            var resetPassword = new ResetPassword(account);

            code = resetPassword.Code;

            await resetPasswordRepository
                .CreateAsync(resetPassword)
                .ConfigureAwait(false);
        }
        else
        {
            await resetPasswordRepository
                .UpdateCodeByIdAsync(
                oldResetPassword.Id,
                code)
                .ConfigureAwait(false);
        }

        //TODO: send email
    }

    public async Task<ResetPassword> GetActiveByIdAsync(string id)
    {
        var resetPassword = await resetPasswordRepository
            .GetActiveByIdAsync(id)
            .ConfigureAwait(false);

        if (resetPassword.HasExpired())
        {
            await resetPasswordRepository
                .UpdateStatusByIdAsync(
                id,
                ResetPasswordStatus.Expired)
                .ConfigureAwait(false);

            throw new SpecificResourceNotFoundException<ResetPassword>(nameof(ResetPassword.Id), id);
        }

        return resetPassword;
    }
}
