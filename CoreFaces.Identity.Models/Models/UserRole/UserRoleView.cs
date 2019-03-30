using CoreFaces.Identity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Identity.Models.UserRole
{
    public class UserRoleView : EntityBase
    {
        public string Name { get; set; }
        public string RoleDescription { get; set; }
        public Guid OwnerId { get; set; } //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
