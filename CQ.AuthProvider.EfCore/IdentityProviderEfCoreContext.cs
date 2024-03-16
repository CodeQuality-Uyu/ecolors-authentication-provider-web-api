using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.EfCore
{
    public sealed class IdentityProviderEfCoreContext : EfCoreContext
    {
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
                    "admin@gmail.com",
                    "!12345678")
                {
                    Id = "d4702564-8273-495b-a694-82fcc69da874"
                });
        }
    }
}
