using System;
using CoreFaces.Identity.Models.Domain;
using System.Collections.Generic;
using CoreFaces.Identity.Models.UserRole;

namespace CoreFaces.Identity.Models.Users
{
    public class UserPasswordChanceView : EntityBase
    {
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string NewPasswordTwo { get; set; } = "";
    }
}
