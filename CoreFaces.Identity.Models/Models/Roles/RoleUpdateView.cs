using System;

namespace CoreFaces.Identity.Models.Roles
{
    public class RoleUpdateView
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
