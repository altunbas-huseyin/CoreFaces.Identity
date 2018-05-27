using System;

namespace CoreFaces.Identity.Models.Domain
{
    public class RolePermission : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public Permission Permission { get; set; }
    }
}
