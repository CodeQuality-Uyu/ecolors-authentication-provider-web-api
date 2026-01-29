using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Suscriptions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Suscriptions;

internal sealed class SuscriptionRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper) 
    : EfCoreRepository<SuscriptionEfCore>(context), ISuscriptionRepository
{
    public async Task<Suscription> CreateAsync(App app)
    {
        var suscriptionEfCore = new SuscriptionEfCore
        {
            AppId = app.Id,
        };

        await CreateAsync(suscriptionEfCore).ConfigureAwait(false);

        var suscription = mapper.Map<Suscription>(suscriptionEfCore);
        
        return suscription;
    }

    public async Task<Suscription> GetByValueAsync(string value)
    {
        var suscriptionEfCore = await Entities
            .AsNoTracking()
            .Where(s => s.Value == value)
            .Include(s => s.App)
            .AsSplitQuery()
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(suscriptionEfCore, value, nameof(Suscription.Value));

        var suscription = mapper.Map<Suscription>(suscriptionEfCore);

        return suscription; 
    }
}