using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Roles;
using CoreFaces.Identity.Helper;
using CoreFaces.Identity.Filters;
using CoreFaces.Identity.Models.Models.Roles;
using CoreFaces.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFaces.Identity.SystemAdmin.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/SystemAdmin/[controller]")]
    [ValidateModel("1c823a7d-7475-4c09-ad13-3b94a53ca943,57daa98a-3c56-4f0e-9247-3a07ac1b4c08")]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IdentityDatabaseContext _identityDatabaseContext;
        Jwt jwt = new Jwt();

        public RolesController(IdentityDatabaseContext identityDatabaseContext, IRoleService roleService, IUserRoleService userRoleService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }


        [HttpGet]
        public CommonApiResponse<List<RoleView>> Get()
        {
            jwt = ViewBag.Jwt;
            List<RoleView> roleList = _roleService.GetByUserId(jwt.UserId);
            return CommonApiResponse<List<RoleView>>.Create(Response, System.Net.HttpStatusCode.OK, true, roleList, null);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CommonApiResponse<Role> Get(Guid id)
        {
            jwt = ViewBag.Jwt;
            Role role = _roleService.GetById(jwt.UserId, id);
            return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse<Role> Post(RoleRegisterView roleRegisterView)
        {
            jwt = ViewBag.Jwt;
            Role role = new Role();
            role.Name = roleRegisterView.Name;
            role.UserId = jwt.UserId;
            role.Description = roleRegisterView.Description;
            role.StatusId = 2;//Active

            Guid insertId = _roleService.Save(role);
            bool result = Guid.TryParse(insertId.ToString(), out insertId);
            if (result)
            {
                return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
            }
            else
            {
                return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, false, new Role(), FluentValidationHelper.GenerateErrorList("An error occurred."));
            }
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse<Role> Put(RoleUpdateView roleUpdateView)
        {
            jwt = ViewBag.Jwt;
            Role role = _roleService.GetById(jwt.UserId, roleUpdateView.Id);
            if (role == null)
            {
                return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Role not found."));
            }

            role.Name = roleUpdateView.Name;
            role.Description = roleUpdateView.Description;
            bool result = _roleService.Update(role);

            if (result)
            {
                return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
            }
            else
            {
                return CommonApiResponse<Role>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse<string> Delete(Guid userId, Guid id)
        {
            jwt = ViewBag.Jwt;

            List<UserRole> list = _userRoleService.GetByRoleId(id);
            if (list.Count > 0)
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.InternalServerError, false, null, FluentValidationHelper.GenerateErrorList("This role can not be deleted because it is used."));
            }

            bool result = _roleService.Delete(id, userId);
            if (result)
            { return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başarılı", null); }

            return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.InternalServerError, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
        }
    }
}
