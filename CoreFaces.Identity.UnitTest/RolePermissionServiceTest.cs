using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Controllers;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Services;
using System;

namespace CoreFaces.Identity.UnitTest
{
    
    [TestClass]
    public class RolePermissionServiceTest : BaseTest
    {

        RolePermission rolePermission = new RolePermission();

        public RolePermissionServiceTest()
        {
            rolePermission = new RolePermission();
            rolePermission.Id = Guid.Parse("11127a7e-eb62-442b-b0dd-05cc0102ebc1");
            //permission.OwnerId = "21127a7e-eb62-442b-b0dd-05cc0102ebc1"; //OwnerId sahip kullanýcý yani AppAdmin rolüne sahip olan kullanýcýdýr.
            rolePermission.UserId = Guid.Parse("31127a7e-eb62-442b-b0dd-05cc0102ebc1");
            rolePermission.PermissionId = Guid.Parse("11127a7e-eb62-442b-b0dd-05cc0102ebc1");
            rolePermission.RoleId = Guid.Parse("51127a7e-eb62-442b-b0dd-05cc0102ebc1");
        }

       
        [TestMethod]
        public void AddRolePermission()
        {
           Guid insertId  = _rolePermissionService.Save(rolePermission);
            bool result = Guid.TryParse(insertId.ToString(), out insertId);
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void UpdateRolePermission()
        {
            bool result = _rolePermissionService.Update(rolePermission);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void DeleteRolePermission()
        {
            bool result = _rolePermissionService.Delete(rolePermission.UserId, rolePermission.Id);
            Assert.AreEqual(result, true);
        }


    }
}
