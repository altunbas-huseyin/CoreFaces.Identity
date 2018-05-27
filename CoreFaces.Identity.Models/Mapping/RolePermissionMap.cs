using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Identity.Models.Domain;

namespace CoreFaces.Identity.Models.Mapping
{
   public class RolePermissionMap
    {
        public RolePermissionMap(EntityTypeBuilder<RolePermission> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.PermissionId).IsRequired();
            entityBuilder.Property(t => t.RoleId).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();

            //Releations
            entityBuilder.HasOne(p => p.Permission).
                WithMany(p => p.RolePermissios);

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.PermissionId, t.RoleId,t.UserId }).IsUnique();
        }
    }
}
