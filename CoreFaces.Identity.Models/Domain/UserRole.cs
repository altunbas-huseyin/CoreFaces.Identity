using System;

namespace CoreFaces.Identity.Models.Domain
{
    public class UserRole : EntityBase
    {
        public Guid OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; }
    }
}
