using System;

namespace CoreFaces.Identity.Models.RolePermissions
{
    public class RolePermissionCrudView
    {
        public Guid OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
    }
}
