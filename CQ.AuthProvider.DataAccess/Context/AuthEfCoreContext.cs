using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.DataAccess.Context
{
    public sealed class AuthEfCoreContext : EfCoreContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<ResetPasswordApplication> ResetPasswordApplications { get; set; }

        public AuthEfCoreContext(DbContextOptions<AuthEfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountId = "d4702564-8273-495b-a694-82fcc69da874";
            var roleId = Guid.NewGuid().ToString();
            var permissionId = Guid.NewGuid().ToString();

            var permission = new Permission
            {
                Id = permissionId,
                Name = "Crear permiso",
                Description = "Crear permiso",
                Key = "create-permission",
                IsPublic = false,
            };

            var role = new Role
            {
                Id = roleId,
                Name = "Admin",
                Description = "Admin",
                Key = "admin",
                IsPublic = false
            };

            var account = new Account
            {
                Id = accountId,
                Email = "admin@gmail.com"
            };

            modelBuilder
                .Entity<Account>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>(
                r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId)
                )
                .HasData(new AccountRole
                {
                    AccountId = accountId,
                    RoleId = roleId,
                });

            modelBuilder
                .Entity<Role>()
                .HasMany(a => a.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                r => r.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId),
                l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId)
                ).HasData(

                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                });

            modelBuilder.Entity<Permission>().HasData(permission);
            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<Account>().HasData(account);

        }
    }

    public sealed record class AccountRole
    {
        public string AccountId { get; set; }

        public string RoleId { get; set; }

        public Account Account { get; set; }

        public Role Role { get; set; }
    }

    public sealed record class RolePermission
    {
        public string RoleId { get; set; }

        public string PermissionId { get; set; }

        public Role Role { get; set; }

        public Permission Permission { get; set; }
    }
}
