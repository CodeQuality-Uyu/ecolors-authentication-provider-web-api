using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.IdentityProvider.EfCore;

public sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : EfCoreContext(options),
    IIdentityProviderHealthService
{
    public DbSet<Identity> Identities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public string GetProvider()
    {
        return "EfCore";
    }

    public bool Ping()
    {
        return base.Ping();
    }
}
