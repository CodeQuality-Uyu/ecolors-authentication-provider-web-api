using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Mongo
{
    public class IdentityRepository : MongoDriverRepository<Identity>, IIdentityProviderRepository, IIdentityProviderHealthService
    {
        public IdentityRepository(MongoContext context) : base(context) { }

        public new async Task CreateAsync(Identity identity)
        {
            await base.CreateAsync(identity).ConfigureAwait(false);
        }

        public string GetName()
        {
            var databaseInfo = base._mongoContext.GetDatabaseInfo();

            return $"{databaseInfo.Provider}-{databaseInfo.Name}";
        }

        public bool Ping()
        {
            return base._mongoContext.Ping();
        }

        public async Task UpdatePasswordAsync(string identityId, string newPassword)
        {
            await base.UpdateByPropAsync(identityId, new { Password = newPassword }).ConfigureAwait(false);
        }
    }
}
