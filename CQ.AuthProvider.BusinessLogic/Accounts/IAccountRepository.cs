using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public interface IAccountRepository<TAccount> 
        : IRepository<TAccount>, IAccountInfoRepository
        where TAccount : class
    {
    }
}
