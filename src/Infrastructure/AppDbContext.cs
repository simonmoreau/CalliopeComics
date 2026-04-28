using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Common.Interfaces;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<CLIENT> CLIENTS { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken, string? userId)
        {
            // Access added entities
            List<object> addedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            // Access modified entities
            List<object> modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            foreach (object? entity in addedEntities)
            {
                if (entity is AuditableEntity auditableEntity)
                {
                    // Set the CreatedBy and CreatedDate properties
                    auditableEntity.CreatedBy = userId; // Replace with actual user ID
                    auditableEntity.Created = DateTime.UtcNow;
                    auditableEntity.LastModifiedBy = userId;
                    auditableEntity.LastModified = DateTime.UtcNow;
                }
            }

            foreach (object? entity in modifiedEntities)
            {
                if (entity is AuditableEntity auditableEntity)
                {
                    // Set the LastModifiedBy and LastModified properties
                    auditableEntity.LastModifiedBy = userId; // Replace with actual user ID
                    auditableEntity.LastModified = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ValueComparer<string[]> stringsComparer = new ValueComparer<string[]>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToArray());

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

        }

    }
}