using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.UnitOfWork.EfCore.Core;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps
{
    internal sealed class AppRepository(
        AuthDbContext context,
        IMapper mapper)
        : EfCoreRepository<AppEfCore>(context),
        IAppRepository
    {
        public async Task<List<App>> GetByIdsAsync(List<string> ids)
        {
            var apps = await GetAllAsync(a => ids.Contains(a.Id))
                .ConfigureAwait(false);

            return mapper.Map<List<App>>(apps);
        }

        async Task<App> IAppRepository.GetByIdAsync(string id)
        {
            var app = await GetByIdAsync(id)
                .ConfigureAwait(false);

            return mapper.Map<App>(app);
        }
    }
}
