using System;
using System.ComponentModel.DataAnnotations;

namespace CoreFaces.Identity.Models.Domain
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int StatusId { get; set; } = 0;
        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
