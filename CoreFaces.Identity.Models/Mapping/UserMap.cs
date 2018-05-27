using CoreFaces.Identity.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace CoreFaces.Identity.Models.Mapping
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.SurName).IsRequired();
            entityBuilder.Property(t => t.IdentityNumber).IsRequired();
            entityBuilder.Property(t => t.ParentId).IsRequired();
            entityBuilder.Property(t => t.AreaId);
            entityBuilder.Property(t => t.AuthorityId);
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.Property(t => t.BirthDate);
            entityBuilder.Property(t => t.FirmName);
            entityBuilder.Property(t => t.SectionName);
            entityBuilder.Property(t => t.Title);
            entityBuilder.Property(t => t.CustomerId);
            entityBuilder.Property(t => t.Extra1);
            entityBuilder.Property(t => t.Extra2);
            entityBuilder.Property(t => t.Gsm);
            //entityBuilder.Property(t => t.Roles).IsRequired();

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.Email, t.ParentId }).IsUnique();


        }
    }
}
