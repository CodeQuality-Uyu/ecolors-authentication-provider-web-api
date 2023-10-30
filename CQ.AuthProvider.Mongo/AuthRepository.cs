using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.MongoDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Mongo
{
    public class AuthRepository : MongoDriverRepository<Auth>, IAuthRepository
    {
        public AuthRepository(MongoContext context) : base(context, "Auths") { }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await base.ExistAsync(a => a.Email == email).ConfigureAwait(false);
        }

        public async Task<Auth> GetByEmailAsync(string email)
        {
            var auth = await base.GetOrDefaultAsync(a => a.Email == email).ConfigureAwait(false);

            if (auth == null)
            {
                throw new AuthNotFoundException(email);
            }

            return auth;
        }

        public async Task UpdatePasswordAsync(string newPassword, Auth auth)
        {
            await base.UpdateByPropAsync(auth.Id, new { Password = newPassword }).ConfigureAwait(false);
        }
    }
}
