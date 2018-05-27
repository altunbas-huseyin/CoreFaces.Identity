using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Models.Domain;
using System;


namespace CoreFaces.Identity.UnitTest
{

    [TestClass]
    public class PermissionServiceTest : BaseTest
    {
        Permission permission = new Permission();
        public PermissionServiceTest()
        {
            permission = new Permission { UserId = Guid.Parse("11127a7e-eb62-442b-b0dd-05cc0102ebc1"), Id = Guid.Parse("11127a7e-eb62-442b-b0dd-05cc0102ebc1"), Name = "Test", Description = "Test" };
        }

        [TestMethod]
        public void AddPermission()
        {
            if (_permissionService.GetById(permission.Id) == null)
            {
                Guid insertGuid = _permissionService.Save(permission);
                bool result = Guid.TryParse(insertGuid.ToString(), out insertGuid);
                Assert.AreEqual(result, true);
            }
        }


        [TestMethod]
        public void UpdatePermission()
        {
            bool result = _permissionService.Update(permission);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void DeletePermission()
        {
            // Result<Permission> result = _permissionService.Delete(permission.UserId, permission.Id);
            // Assert.AreEqual(result.Status, true);
        }


    }
}
