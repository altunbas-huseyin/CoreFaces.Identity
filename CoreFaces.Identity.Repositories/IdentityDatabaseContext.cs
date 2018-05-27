using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreFaces.Identity.Models.Mapping;
using CoreFaces.Identity.Models.Domain;
using System.Threading;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreFaces.Identity.Repositories
{
    public class IdentityDatabaseContext : DbContext
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
        {
            //bool status = this.Database.EnsureDeleted();
            //IExecutionStrategy dd = this.Database.CreateExecutionStrategy();
            //bool test = this.Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new RoleMap(modelBuilder.Entity<Role>());
            new JwtMap(modelBuilder.Entity<Jwt>());
            new UserRoleMap(modelBuilder.Entity<UserRole>());
            new PermissionMap(modelBuilder.Entity<Permission>());
            new RolePermissionMap(modelBuilder.Entity<RolePermission>());

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var changeSet = ChangeTracker.Entries<EntityBase>();
            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.CreateDate = DateTime.Now;
                    }
                    entry.Entity.UpdateDate = DateTime.Now;
                }
            }
        }
    }
}
