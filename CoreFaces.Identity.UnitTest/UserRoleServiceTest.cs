using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Controllers;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Services;
using System;

namespace CoreFaces.Identity.UnitTest
{

    [TestClass]
    public class UserRoleServiceTest : BaseTest
    {
        UserRole userRole = new UserRole { Id = Guid.Parse("381804e9-a3a6-4197-8a0f-05680244b01a"), UserId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"),  RoleId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), OwnerId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943") };

        [TestMethod]
        public void AddSystemUserUserRoles()
        {
            if (_userRoleService.GetById(userRole.Id) == null)
                _userRoleService.Save(userRole);
        }
    }
}
