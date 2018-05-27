using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Identity.Models.Domain;

namespace CoreFaces.Identity.Models.Mapping
{
    public class UserRoleMap
    {
        public UserRoleMap(EntityTypeBuilder<CoreFaces.Identity.Models.Domain.UserRole> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.RoleId).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();

            //Releations
            entityBuilder.
                HasOne(p => p.User).
                WithMany(p => p.UserRoles);

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.UserId, t.RoleId }).IsUnique();
        }
    }
}
