using System;

namespace CoreFaces.Identity.Models.Roles
{
    public class RoleRegisterView
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
