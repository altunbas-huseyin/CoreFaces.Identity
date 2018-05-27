using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Controllers;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Services;
using System;

namespace CoreFaces.Identity.UnitTest
{
    
    [TestClass]
    public class RoleServiceTest : BaseTest
    {

        [TestMethod]
        public void InsertRoleList()
        {
            Role roleSystemAdmin = new Role { Id = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), UserId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), Name = "SystemAdmin", Description = "Tüm sistemi kullanýcýlarý yönetebilen kullanýcý." };
            Role roleAppAdmin = new Role { Id = Guid.Parse("57daa98a-3c56-4f0e-9247-3a07ac1b4c08"), UserId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), Name = "AppAdmin", Description = "X isimli bir proje üyelerinin yönetilebileceði bir hesap." };
            Role roleAppUser = new Role { Id = Guid.Parse("44211fbb-ed8a-405d-a639-9919f5fbbb3e"), UserId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), Name = "AppUser", Description = "Herhangi bir AppAdmin kullanýcýsýnýn oluþturduðu kullanýcýlar." };

            if (_roleService.GetByUserIdAndName(Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), "SystemAdmin") == null)
                _roleService.Save(roleSystemAdmin);

            if (_roleService.GetByUserIdAndName(Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), "AppAdmin") == null)
                _roleService.Save(roleAppAdmin);

            if (_roleService.GetByUserIdAndName(Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), "AppUser") == null)
                _roleService.Save(roleAppUser);

        }


    }
}
