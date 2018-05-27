using CoreFaces.Identity.Models.Domain;
using System;

namespace CoreFaces.Identity.Models.Users
{
    public class UserUpdateView : EntityBase
    {
        public Guid AuthorityId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Gsm { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public dynamic Extra1 { get; set; } = "";
        public dynamic Extra2 { get; set; } = "";
        public string FirmName { get; set; } = "";
        public string Title { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string CustomerId { get; set; } = "";
        public string IdentityNumber { get; set; }
        public Guid AreaId { get; set; }
        public DateTime StartDate { get; set; } = Convert.ToDateTime("01.01.1700");
        public DateTime EndDate { get; set; } = Convert.ToDateTime("01.01.1700");
    }
}
