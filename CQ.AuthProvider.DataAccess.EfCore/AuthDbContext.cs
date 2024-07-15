using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : EfCoreContext(options)
{
    public DbSet<TenantEfCore> Tenants { get; set; }

    public DbSet<AccountEfCore> Accounts { get; set; }

    public DbSet<AccountRole> AccountsRoles { get; set; }

    public DbSet<AccountApp> AccountsApps { get; set; }

    public DbSet<RoleEfCore> Roles { get; set; }

    public DbSet<RolePermission> RolesPermissions { get; set; }

    public DbSet<RoleApp> RolesApps { get; set; }

    public DbSet<PermissionEfCore> Permissions { get; set; }

    public DbSet<PermissionApp> PermissionsApps { get; set; }

    public DbSet<SessionEfCore> Sessions { get; set; }

    public DbSet<ResetPasswordEfCore> ResetPasswords { get; set; }

    public DbSet<AppEfCore> Apps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TenantEfCore>(entity =>
        {
            entity
            .HasMany(t => t.Accounts)
            .WithOne(a => a.Tenant)
            .HasForeignKey(a => a.TenantId);
        });

        #region Account
        modelBuilder.Entity<AccountEfCore>(entity =>
        {
            entity
            .HasOne(a => a.Tenant)
            .WithMany(t => t.Accounts)
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity
            .HasOne(a => a.Account)
            .WithMany(a => a.Roles)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(a => a.Role)
            .WithMany(r => r.Accounts)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(a => a.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<AccountApp>(entity =>
        {
            entity
            .HasOne(a => a.App)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(a => a.Account)
            .WithMany(a => a.Apps)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(a => a.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region App
        modelBuilder.Entity<AppEfCore>(entity =>
        {
        });

        modelBuilder.Entity<PermissionApp>(entity =>
        {
            entity
            .HasOne(p => p.App)
            .WithMany(a => a.Permissions)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(p => p.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<RoleApp>(entity =>
        {
            entity
            .HasOne(r => r.Role)
            .WithMany(r => r.Apps)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(r => r.App)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(r => r.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region Role

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity
            .HasOne(r => r.Role)
            .WithMany(p => p.Permissions)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(r => r.Permission)
            .WithMany(p => p.Roles)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(r => r.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion
    }
}
