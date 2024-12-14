using AutoMapper;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;

internal sealed class ResetPasswordRepository(
    AuthDbContext _context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper _mapper)
    : EfCoreRepository<ResetPasswordEfCore>(_context),
    IResetPasswordRepository
{
    public async Task<ResetPassword> GetActiveForAcceptanceAsync(
        Guid id,
        string email,
        int code)
    {
        var query = Entities
            .Where(r => r.Id == id)
            .Where(r => r.Account.Email == email)
            .Where(r => r.Code == code)
            .Where(r => DateTimeOffset.UtcNow <= r.ExpiresAt);

        var resetPassword = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(resetPassword, id, nameof(ResetPassword.Id));

        return _mapper.Map<ResetPassword>(resetPassword);
    }

    public async Task<ResetPassword?> GetOrDefaultByEmailAsync(string email)
    {
        var query = Entities
            .Include(r => r.Account)
            .Where(r => r.Account.Email == email);

        var resetPassword = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return _mapper.Map<ResetPassword>(resetPassword);
    }

    async Task IResetPasswordRepository.CreateAndSaveAsync(ResetPassword resetPassword)
    {
        var resetPasswordEfCore = new ResetPasswordEfCore(
            resetPassword.Id,
            resetPassword.Code,
            resetPassword.Account.Id);

        await CreateAndSaveAsync(resetPasswordEfCore).ConfigureAwait(false);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await DeleteAndSaveAsync(r => r.Id == id)
            .ConfigureAwait(false);
    }

    public async Task UpdateCodeByIdAsync(
        Guid id,
        int code)
    {
        var resetPassword = await base.GetByIdAsync(id).ConfigureAwait(false);

        resetPassword.Code = code;

        await UpdateAndSaveAsync(resetPassword).ConfigureAwait(false);
    }
}
