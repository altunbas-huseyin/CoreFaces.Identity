using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Identity.Models.Domain;

namespace CoreFaces.Identity.Models.Mapping
{
    public class PermissionMap
    {
        public PermissionMap(EntityTypeBuilder<Permission> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.UserId, t.Name }).IsUnique();

           
        }
    }
}
