using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Identity.Models.Domain;

namespace CoreFaces.Identity.Models.Mapping
{
    public class JwtMap
    {
        public JwtMap(EntityTypeBuilder<Jwt> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.Token).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
            entityBuilder.Property(t => t.DeadLine).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
        }
    }
}
