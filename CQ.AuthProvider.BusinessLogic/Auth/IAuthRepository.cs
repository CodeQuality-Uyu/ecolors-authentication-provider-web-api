using CQ.AuthProvider.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface IAuthRepository
    {
        Task<Auth> GetByEmailAsync(string email);

        Task<bool> ExistByEmailAsync(string email);

        Task<Auth> CreateAsync(Auth auth);

        Task UpdateAsync(Auth auth);
    }
}
