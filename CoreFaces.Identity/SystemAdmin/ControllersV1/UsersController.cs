using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using CoreFaces.Identity.Filters;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Identity.Helper;
using CoreFaces.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFaces.Identity.SystemAdmin.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/SystemAdmin/[controller]")]
    [ValidateModel("1c823a7d-7475-4c09-ad13-3b94a53ca943,57daa98a-3c56-4f0e-9247-3a07ac1b4c08")]
    public class UsersController : BaseController
    {
        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IUserService _userService;
        private readonly IJwtService _iJwtService;
        Jwt jwt = new Jwt();

        public UsersController(IdentityDatabaseContext identityDatabaseContext, IUserService testService, IJwtService iJwtService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _userService = testService;
            _iJwtService = iJwtService;
        }

        // GET api/values
        [HttpGet]
        public CommonApiResponse<List<User>> Get()
        {
            jwt = ViewBag.Jwt;
            List<User> userList = _userService.GetByParentId(jwt.UserId);

            return CommonApiResponse<List<User>>.Create(Response, System.Net.HttpStatusCode.OK, true, userList, null);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CommonApiResponse<UserView> Get(Guid id)
        {
            jwt = ViewBag.Jwt;
            UserView userView = _userService.GetUserViewById(jwt.UserId, id);
            return CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.OK, true, userView, null);
        }


        // POST api/values
        [HttpPost]
        public CommonApiResponse<User> Post(UserRegisterView userView)
        {
            try
            {
                jwt = ViewBag.Jwt;
                if (_userService.GetByEmail(userView.Email) != null)
                {
                    return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.Conflict, false, null, "This e-mail address is registered with our system.");
                }

                User user = new User();

                user.ParentId = jwt.UserId;
                user.Email = userView.Email;
                user.Password = userView.Password;
                user.Name = userView.Name;
                user.SurName = userView.SurName;
                user.StatusId = 2;//Active
                // user.Role = new List<Role>();

                List<ValidationFailure> list = UserValidator.FieldValidate(user).ToList();
                if (list.Count > 0)
                {
                    return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.NotFound, false, null, list);
                }

                Guid insertId = _userService.Save(user);
                bool result = Guid.TryParse(insertId.ToString(), out insertId);

                return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
            }
            catch (Exception ex)
            {
                return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.NoContent, false, null, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse<User> Put(UserUpdateView userUpdateView)
        {
            jwt = ViewBag.Jwt;
            User user = _userService.GetById(jwt.UserId, userUpdateView.Id);

            if (user == null)
            {
                return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.Conflict, false, null, "No members found.");
            }

            user.Email = userUpdateView.Email;
            user.Name = userUpdateView.Name;
            user.SurName = userUpdateView.SurName;
            user.Extra1 = userUpdateView.Extra1;
            user.Extra2 = userUpdateView.Extra2;

            bool result = _userService.Update(user);
            if (!result)
            {
                return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
            }

            //return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
            return CommonApiResponse<User>.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
        }

        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse<string> Delete(UserUpdateView userUpdateView)
        {
            jwt = ViewBag.Jwt;
            User user = _userService.GetById(jwt.UserId, userUpdateView.Id);
            if (user == null)
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.Conflict, false, null, "User not found.");
            }

            bool result = _userService.Delete(user.Id);
            if (result)
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.OK, true, "Succsess", null);
            }
            else
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.Conflict, false, null, "An error occurred.");
            }
        }
    }
}
