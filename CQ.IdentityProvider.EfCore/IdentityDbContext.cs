using CQ.AuthProvider.BusinessLogic.Identities;
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
        modelBuilder.Entity<Identity>(entity =>
        {
            entity
            .HasData(
                new Identity
                {
                    Id = Guid.Parse("0EE82EE9-F480-4B13-AD68-579DC83DFA0D"),
                    Email = "seed@cq.com",
                    Password = "!12345678"
                });
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
