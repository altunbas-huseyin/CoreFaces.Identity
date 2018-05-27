using System;

namespace CoreFaces.Identity.Models.Domain
{
    public class Jwt : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
