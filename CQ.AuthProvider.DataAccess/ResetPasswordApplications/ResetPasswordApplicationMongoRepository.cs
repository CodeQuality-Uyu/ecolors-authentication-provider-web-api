using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.UnitOfWork.EfCore;
using CQ.UnitOfWork.MongoDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.ResetPasswordApplications
{
    internal sealed class ResetPasswordApplicationMongoRepository
        : MongoDriverRepository<ResetPasswordApplication>,
        IResetPasswordApplicationRepository<ResetPasswordApplication>
    {
        private readonly IMapper _mapper;

        public ResetPasswordApplicationMongoRepository(
            MongoContext mongoContext,
            IMapper mapper) : base(mongoContext)
        {
            this._mapper = mapper;
        }

        public async Task DeleteByIdAsync(string id)
        {
            await base.DeleteAsync(r => r.Id == id).ConfigureAwait(false);
        }

        public async Task<ResetPasswordApplication> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception
        {
            return await base.GetByIdAsync(id, exception).ConfigureAwait(false);
        }

        public async Task<ResetPasswordApplication?> GetOrDefaultInfoByAccountEmailAsync(string email)
        {
            return await base.GetOrDefaultByPropAsync(
                email,
                $"{nameof(ResetPasswordApplication.Account)}.{nameof(AccountEfCore.Email)}")
                .ConfigureAwait(false);
        }
    }
}
