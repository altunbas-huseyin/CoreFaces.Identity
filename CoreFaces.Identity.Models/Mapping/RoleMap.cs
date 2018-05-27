using CoreFaces.Identity.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFaces.Identity.Models.Mapping
{
    public class RoleMap
    {
        public RoleMap(EntityTypeBuilder<Role> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.UserId, t.Name }).IsUnique();
        }
    }
}
