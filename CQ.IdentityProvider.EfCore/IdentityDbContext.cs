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
        modelBuilder
            .Entity<Identity>(entity =>
            {
                entity.HasData(
                new Identity(
                    "5f7dd4f88608458ea68bdc3ef9a94e59",
                    "admin@gmail.com",
                    "!12345678"));
            });
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
