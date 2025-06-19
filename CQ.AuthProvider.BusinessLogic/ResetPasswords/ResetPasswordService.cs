using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Emails;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

internal sealed class ResetPasswordService(
    IResetPasswordRepository _resetPasswordRepository,
    IIdentityRepository _identityRepository,
    IAccountRepository _accountRepository,
    IEmailService _emailService)
    : IResetPasswordService
{
    public async Task CreateAsync(CreateResetPasswordArgs args)
    {
        var oldResetPassword = await _resetPasswordRepository
            .GetOrDefaultByEmailAsync(args.Email)
            .ConfigureAwait(false);

        int code;
        if (Guard.IsNull(oldResetPassword))
        {
            var account = await _accountRepository
                .GetByEmailAsync(args.Email)
                .ConfigureAwait(false);

            var resetPassword = ResetPassword.New(account);

            code = resetPassword.Code;

            await _resetPasswordRepository
                .CreateAndSaveAsync(resetPassword)
                .ConfigureAwait(false);
        }
        else
        {
            code = ResetPassword.NewCode();

            await _resetPasswordRepository
                .UpdateCodeByIdAsync(
                oldResetPassword.Id,
                code)
                .ConfigureAwait(false);
        }

        await _emailService
            .SendAsync(
            args.Email,
            EmailTemplateKey.ResetPassword,
            new
            {
                code
            })
            .ConfigureAwait(false);
    }

    public async Task AcceptAsync(
        Guid id,
        AcceptResetPasswordArgs args)
    {
        var resetPasswordOldApplication = await _resetPasswordRepository
            .GetActiveForAcceptanceAsync(
            id,
            args.Email,
            args.Code)
            .ConfigureAwait(false);

        await _identityRepository
            .UpdatePasswordByIdAsync(
            resetPasswordOldApplication.Account.Id,
            string.Empty,
            args.NewPassword)
            .ConfigureAwait(false);

        await _resetPasswordRepository
            .DeleteByIdAsync(id)
            .ConfigureAwait(false);
    }
}
