using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Licenses;
using CQ.AuthProvider.BusinessLogic.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Licenses;

internal sealed class LicenseRepository(
    AuthDbContext _context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper _mapper)
    : AuthDbContextRepository<LicenseEfCore>(_context),
    ILicenseRepository
{
    public async Task CreateAsync(License license)
    {
        var licenseEfCore = _mapper.Map<LicenseEfCore>(license);

        await CreateAsync(licenseEfCore);
    }
}
