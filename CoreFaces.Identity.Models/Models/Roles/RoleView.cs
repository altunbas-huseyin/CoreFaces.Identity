using CoreFaces.Identity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Identity.Models.Models.Roles
{
    public class RoleView : Role
    {
        public List<RolePermission> RolePermissionList { get; set; }
    }
}
