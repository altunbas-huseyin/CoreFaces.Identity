using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Models.Domain
{
    public class User : EntityBase
    {
        public Guid ParentId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid AreaId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        /// <summary>
        /// Kullanıcı yetkilisi Id
        /// </summary>
        public Guid AuthorityId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gsm { get; set; } = "";
        public string FirmName { get; set; } = "";
        /// <summary>
        /// Tc kimlik no 
        /// </summary>
        public string IdentityNumber { get; set; } = "";
        /// <summary>
        /// Ünvan
        /// </summary>
        public string Title { get; set; } = "";
        /// <summary>
        /// Bölüm Adı
        /// </summary>
        public string SectionName { get; set; } = "";
        public string CustomerId { get; set; } = "";
        public string Extra1 { get; set; } = "";
        public string Extra2 { get; set; } = "";
        public ICollection<UserRole> UserRoles { get; set; }
        public DateTime StartDate { get; set; } = Convert.ToDateTime("01.01.1700");
        public DateTime EndDate { get; set; } = Convert.ToDateTime("01.01.1700");
    }
}
