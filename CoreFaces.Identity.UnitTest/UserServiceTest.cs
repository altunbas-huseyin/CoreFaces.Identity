using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Users;

namespace CoreFaces.Identity.UnitTest
{
    [TestClass]
    public class UserServiceTest : BaseTest
    {
        private User user = null;
        public UserServiceTest()
        {
            
            Role role = _roleService.GetByName(Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), "AppAdmin");

            user = new User();
            user.Id = Guid.Parse("61e444eb-5e90-448f-a680-8125831df9d1");
            user.Email = "test@test.com";
            user.Password = "1111";
            user.Name = "Hüseyin";
            user.SurName = "Altunbaş";
            user.StatusId = 2;
            //user.Role = new List<Role>();
            //user.Role.Add(role);
        }

        [TestMethod]
        public void AddUser()
        {
            Guid inserId = _userService.Save(user);
            bool result = false;
            result = Guid.TryParse(inserId.ToString(), out inserId);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Get()
        {
            Kendo.DynamicLinq.View filters = new Kendo.DynamicLinq.View();
            var inserId = _userService.Get(filters);
        }

        [TestMethod]
        public void LoginByEmail()
        {
            UserView userView = _userService.LoginByEmail(user.Email, user.Password);
            Assert.AreNotEqual(userView, null);
        }

        [TestMethod]
        public void UserAddJwt()
        {
            //user.Id, Guid.NewGuid().ToString(), DateTime.Now.AddDays(5)
            Jwt j = new Jwt { UserId = user.Id, Token = Guid.NewGuid(), DeadLine = DateTime.Now.AddDays(5) };
            Guid insertGuid = _jwtService.Save(j);
        }

        [TestMethod]
        public void UserJwtTest()
        {
            Jwt jwt = (Jwt)_jwtService.GetByUserId(user.Id).Data;
            Jwt jwtCheckToken = (Jwt)_jwtService.CheckToken(jwt.Token).Data;
        }

        [TestMethod]
        public void AddSystemUser()
        {
            Role role = _roleService.GetByName(Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), "SystemAdmin");
            Assert.AreNotEqual(role, null);

            
            if (_userService.GetByEmail("altunbas.huseyin@gmail.com") == null)
            {
                User user = new User();
                user.Id = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943");
                user.Email = "altunbas.huseyin@gmail.com";
                user.ParentId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                user.Password = "1111";
                user.Name = "Hüseyin";
                user.SurName = "Altunbaş";
                user.StatusId = 2;
                //user.Role = new List<Role>();
                //user.Role.Add(role);

                _userService.Save(user);
            }

            User _user = _userService.GetByParentIdAndId(Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"));

        }

        //[TestMethod]
        //public void Login()
        //{
        //    Jwt jwt = (Jwt)_jwtService.GetByUserId(user.Id).Data;
        //    LoginController userController = new LoginController(_identityDatabaseContext, _userService, _statusService);
        //    UserView userView = _userService.LoginByEmail(user.Email, user.Password);

        //    Assert.AreNotEqual(userView, null);

        //}

        //[TestMethod]
        // public void Register()
        // {
        //     UsersController userController = new UsersController();
        //     UserRegisterView _user = new UserRegisterView();
        //     _user.Email = "Huseyin";
        //     _user.Password = Encripty.EncryptString("1111");
        //     _user.Name = "Hüseyin";
        //     _user.SurName = "Altunbaş";
        //
        //     var result = userController.Post(_user);
        // }


        [TestMethod]
        public void DeleteUser()
        {
            bool result = _userService.Delete(user.Id);
            Assert.AreEqual(result, true);

        }

    }
}
