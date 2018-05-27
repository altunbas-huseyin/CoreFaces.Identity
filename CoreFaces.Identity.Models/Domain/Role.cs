using System;

namespace CoreFaces.Identity.Models.Domain
{
    public class Role : EntityBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
