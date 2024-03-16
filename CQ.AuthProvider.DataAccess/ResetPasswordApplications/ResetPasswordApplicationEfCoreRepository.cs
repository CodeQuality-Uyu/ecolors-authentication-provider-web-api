using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.UnitOfWork.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.ResetPasswordApplications
{
    internal sealed class ResetPasswordApplicationEfCoreRepository
        : EfCoreRepository<ResetPasswordApplicationEfCore>,
        IResetPasswordApplicationRepository<ResetPasswordApplicationEfCore>
    {
        private readonly IMapper _mapper;

        public ResetPasswordApplicationEfCoreRepository(
            EfCoreContext efCoreContext,
            IMapper mapper) : base(efCoreContext)
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
            var resetPasswordApplication = await base.GetByIdAsync(id, exception).ConfigureAwait(false);

            return this._mapper.Map<ResetPasswordApplication>(resetPasswordApplication);
        }

        public async Task<ResetPasswordApplication?> GetOrDefaultInfoByAccountEmailAsync(string email)
        {
            var resetPasswordApplication = await base.GetOrDefaultByPropAsync(
                email,
                $"{nameof(ResetPasswordApplicationEfCore.Account)}.{nameof(AccountEfCore.Email)}")
                .ConfigureAwait(false);

            return this._mapper.Map<ResetPasswordApplication?>(resetPasswordApplication);
        }
    }
}
