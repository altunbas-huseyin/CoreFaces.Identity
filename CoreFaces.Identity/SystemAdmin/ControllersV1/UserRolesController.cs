using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Models.Roles;
using CoreFaces.Identity.Helper;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Filters;
using CoreFaces.Identity.Models;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Identity.Models.UserRole;
using CoreFaces.Helper;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFaces.Identity.SystemAdmin.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/SystemAdmin/[controller]")]
    [ValidateModel("1c823a7d-7475-4c09-ad13-3b94a53ca943,57daa98a-3c56-4f0e-9247-3a07ac1b4c08")]
    public class UserRolesController : BaseController
    {
        Jwt jwt = new Jwt();
        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserRolesController(IdentityDatabaseContext identityDatabaseContext, IUserRoleService userRoleService, IUserService userService, IRoleService roleService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _userRoleService = userRoleService;
            _userService = userService;
            _roleService = roleService;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public CommonApiResponse<List<UserRoleView>> Get(Guid id)
        {
            jwt = ViewBag.Jwt;

            List<UserRoleView> userRole = _userRoleService.GetByUserId(jwt.UserId, id);
            return CommonApiResponse<List<UserRoleView>>.Create(Response, System.Net.HttpStatusCode.OK, true, userRole, null);
        }


        // POST api/values
        [HttpPost("{userId}/{roleId}")]
        public CommonApiResponse<UserRole> Post(Guid userId, Guid roleId)
        {
            jwt = ViewBag.Jwt;
            CoreFaces.Helper.Result<UserRole> result = _userRoleService.UserAddRole(jwt.UserId, userId, roleId);
            UserRole userRole = result.Data;
            UserRoleView _userRoleView = _userRoleService.UserRoleToUserRoleView(userRole);
            _userRoleView.Name = _roleService.GetById(userRole.RoleId).Name;

            //return CommonApiResponse<UserRole>.Create(Response, userRole, result);
            return CommonApiResponse<UserRole>.Create(Response, System.Net.HttpStatusCode.OK, true, userRole, null);
        }


        // DELETE api/values/5
        [HttpDelete("{userId}/{roleId}")]
        public CommonApiResponse<UserRole> Delete(Guid userId, Guid roleId)
        {
            jwt = ViewBag.Jwt;
            bool result = _userRoleService.UserRemoveRole(jwt.UserId, userId, roleId);
            return CommonApiResponse<UserRole>.Create(Response, System.Net.HttpStatusCode.OK, result, null, null);
        }
    }
}
