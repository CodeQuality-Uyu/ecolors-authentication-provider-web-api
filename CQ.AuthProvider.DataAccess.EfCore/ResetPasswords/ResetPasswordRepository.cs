using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;

internal sealed class ResetPasswordRepository(
    EfCoreContext context,
    IMapper mapper)
    : EfCoreRepository<ResetPasswordEfCore>(context),
    IResetPasswordRepository
{
    public new async Task<ResetPassword> GetByIdAsync(string id)
    {
        var query = _entities
            .Include(r => r.Account)
            .Where(r => r.Id == id);

        var resetPassword = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(resetPassword, id, nameof(ResetPassword.Id));

        return mapper.Map<ResetPassword>(resetPassword);
    }

    public async Task<ResetPassword?> GetOrDefaultByEmailAsync(string email)
    {
        var query = _entities
            .Include(r => r.Account)
            .Where(r => r.Account.Email == email);

        var resetPassword = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return mapper.Map<ResetPassword>(resetPassword);
    }

    public async Task CreateAsync(ResetPassword resetPassword)
    {
        var resetPasswordEfCore = new ResetPasswordEfCore(
            resetPassword.Id,
            resetPassword.Code,
            resetPassword.Account.Id);

        await CreateAsync(resetPasswordEfCore).ConfigureAwait(false);
    }

    public async Task DeletePendingAsync(
        string id,
        string code)
    {
        await DeleteAndSaveAsync(r =>
        r.Id == id &&
        r.Code == code &&
        r.ExpiresAt <= DateTime.UtcNow)
            .ConfigureAwait(false);
    }

    public async Task UpdateCodeByIdAsync(
        string id,
        string code)
    {
        var resetPassword = await base.GetByIdAsync(id).ConfigureAwait(false);

        resetPassword.Code = code;

        await UpdateAndSaveAsync(resetPassword).ConfigureAwait(false);
    }
}
