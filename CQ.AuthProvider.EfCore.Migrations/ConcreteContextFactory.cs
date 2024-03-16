using CQ.AuthProvider.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CQ.EfCore.Migrations
{
    public class ConcreteContextFactory : IDesignTimeDbContextFactory<IdentityProviderEfCoreContext>
    {
        public IdentityProviderEfCoreContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<IdentityProviderEfCoreContext>()
                .UseSqlServer("Server=localhost;Database=Identity; Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
                .LogTo(Console.WriteLine)
                .Options;

            return new IdentityProviderEfCoreContext(options);
        }
    }
}
