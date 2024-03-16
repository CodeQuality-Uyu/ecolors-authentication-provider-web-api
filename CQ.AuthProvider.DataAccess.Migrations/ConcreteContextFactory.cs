using CQ.AuthProvider.DataAccess.Contexts;
using CQ.AuthProvider.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CQ.EfCore.Migrations
{
    public class ConcreteContextFactory : IDesignTimeDbContextFactory<AuthEfCoreContext>
    {
        public AuthEfCoreContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AuthEfCoreContext>()
                .UseSqlServer("Server=localhost;Database=Auth; Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
                .LogTo(Console.WriteLine)
                .Options;

            return new AuthEfCoreContext(options);
        }
    }
}
