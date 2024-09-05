using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Emails;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;

internal abstract class ResetPasswordService(
    IResetPasswordRepository resetPasswordRepository,
    IIdentityRepository identityRepository,
    IAccountInternalService accountService,
    IEmailService emailService)
    : IResetPasswordService
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
            throw new InvalidOperationException("Invalid reset password application");
        }

        await identityRepository
            .UpdatePasswordByIdAsync(
            resetPasswordOldApplication.Account.Id,
            args.NewPassword)
            .ConfigureAwait(false);

        await resetPasswordRepository
            .DeletePendingAsync(
            id,
            args.Code)
            .ConfigureAwait(false);
    }

    public async Task CreateAsync(string email)
    {
        var oldResetPassword = await resetPasswordRepository
            .GetOrDefaultByEmailAsync(email)
            .ConfigureAwait(false);

        string code;
        if (Guard.IsNull(oldResetPassword))
        {
            var account = await accountService
                .GetByEmailAsync(email)
                .ConfigureAwait(false);

            var resetPassword = new ResetPassword(account);

            code = resetPassword.Code;

            await resetPasswordRepository
                .CreateAsync(resetPassword)
                .ConfigureAwait(false);
        }
        else
        {
            code = ResetPassword.NewCode();

            await resetPasswordRepository
                .UpdateCodeByIdAsync(
                oldResetPassword.Id,
                code)
                .ConfigureAwait(false);
        }

        await emailService
            .SendAsync(
            email,
            EmailTemplateKey.ResetPassword,
            new
            {
                code
            });
    }
}
