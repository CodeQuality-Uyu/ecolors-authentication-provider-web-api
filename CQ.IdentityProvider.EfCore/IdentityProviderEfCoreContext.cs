using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CQ.IdentityProvider.EfCore
{
    public sealed class IdentityProviderEfCoreContext : EfCoreContext
    {
        public const string ADMIN_ID= "d47025648273495ba69482fcc69da874";
        public const string ADMIN_EMAIL = "admin@gmail.com";

        public DbSet<Identity> Identities { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public IdentityProviderEfCoreContext(DbContextOptions<IdentityProviderEfCoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Identity>().HasData(
                new Identity(
                    ADMIN_EMAIL,
                    "!12345678")
                {
                    Id = ADMIN_ID
                });
        }
    }
}
