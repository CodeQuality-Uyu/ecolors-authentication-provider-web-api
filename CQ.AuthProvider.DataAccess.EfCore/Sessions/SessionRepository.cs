
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.UnitOfWork.EfCore.Core;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

internal sealed class SessionRepository(
    EfCoreContext context,
    IMapper mapper)
    : EfCoreRepository<SessionEfCore>(context),
    ISessionRepository
{
}
