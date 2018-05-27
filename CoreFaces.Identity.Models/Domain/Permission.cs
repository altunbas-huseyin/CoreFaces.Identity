using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Models.Domain
{
    public class Permission : EntityBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RolePermission> RolePermissios { get; set; }
    }
}
