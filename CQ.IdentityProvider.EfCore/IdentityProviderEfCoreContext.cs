using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.IdentityProvider.EfCore;

public sealed class IdentityProviderEfCoreContext(DbContextOptions<IdentityProviderEfCoreContext> options)
    : EfCoreContext(options),
    IIdentityProviderHealthService
{
    public const string ADMIN_ID = "d47025648273495ba69482fcc69da874";
    public const string ADMIN_EMAIL = "admin@gmail.com";

    public DbSet<Identity> Identities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Identity>()
            .HasData(
            new Identity(
                ADMIN_EMAIL,
                "!12345678")
            {
                Id = ADMIN_ID
            });
    }

    public string GetProvider()
    {
        return "EfCore";
    }

    public string GetName()
    {
        return string.Empty;
    }

    public bool Ping()
    {
        return base.Ping();
    }
}
