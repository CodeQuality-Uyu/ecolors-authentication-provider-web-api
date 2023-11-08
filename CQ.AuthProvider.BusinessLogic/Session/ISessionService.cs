using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public interface ISessionService
    {
        Task<Session> CreateAsync(CreateSessionCredentials credentials);

        Task<Session> GetByTokenAsync(string token);

        Task<Session?> GetOrDefaultByTokenAsync(string token);

        Task<bool> IsTokenValidAsync(string token);
    }
}
