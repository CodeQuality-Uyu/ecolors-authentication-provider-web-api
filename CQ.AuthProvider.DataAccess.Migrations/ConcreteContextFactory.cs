using CQ.AuthProvider.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CQ.EfCore.Migrations
{
    public class ConcreteContextFactory : IDesignTimeDbContextFactory<AuthEfCoreContext>
    {
        public AuthEfCoreContext CreateDbContext(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var authProviderDbConnectionString = configuration.GetConnectionString("AuthProvider");

            var options = new DbContextOptionsBuilder<AuthEfCoreContext>()
                .UseSqlServer(authProviderDbConnectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new AuthEfCoreContext(options);
        }
    }
}
