using CQ.AuthProvider.EfCore;
using CQ.AuthProvider.EfCore.AppConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CQ.EfCore.Migrations
{
    public class ConcreteContextFactory : IDesignTimeDbContextFactory<IdentityProviderEfCoreContext>
    {
        public IdentityProviderEfCoreContext CreateDbContext(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var identityProviderDbConnectionString = configuration.GetConnectionString("IdentityProvider");

            var options = new DbContextOptionsBuilder<IdentityProviderEfCoreContext>()
                .UseSqlServer(identityProviderDbConnectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new IdentityProviderEfCoreContext(options);
        }
    }
}
