using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    public interface IResetPasswordApplicationRepository<TResetPasswordApplication> : IRepository<TResetPasswordApplication>
        where TResetPasswordApplication: class
    {
        Task<ResetPasswordApplication> GetInfoByIdAsync<TException>(string id, TException exception) where TException : Exception;

        Task DeleteByIdAsync(string id);

        Task<ResetPasswordApplication?> GetOrDefaultInfoByAccountEmailAsync(string email);
    }
}
