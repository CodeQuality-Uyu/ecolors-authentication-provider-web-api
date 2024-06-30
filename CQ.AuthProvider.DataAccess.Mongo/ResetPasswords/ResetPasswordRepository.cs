using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;

internal sealed class ResetPasswordRepository(
AuthDbContext context,
IMapper mapper)
: MongoDriverRepository<ResetPasswordMongo>(context),
IResetPasswordRepository
{
    public Task CreateAsync(ResetPassword resetPassword)
    {
        throw new NotImplementedException();
    }

    public Task<ResetPassword> GetActiveByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ResetPassword> GetByEmailOfAccountAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCodeByIdAsync(string id, string code)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatusByIdAsync(string id, ResetPasswordStatus status)
    {
        throw new NotImplementedException();
    }

    Task<ResetPassword> IResetPasswordRepository.GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
