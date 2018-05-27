using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Identity.Helper;
using CoreFaces.Identity.Filters;
using CoreFaces.Helper;

namespace CoreFaces.Identity.SystemAdmin.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/SystemAdmin/[controller]")]
    [ValidateModel("1c823a7d-7475-4c09-ad13-3b94a53ca943,57daa98a-3c56-4f0e-9247-3a07ac1b4c08")]
    public class RolePermissionController : BaseController
    {
        Jwt jwt = new Jwt();

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IdentityDatabaseContext identityDatabaseContext, IRolePermissionService rolePermissionService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _rolePermissionService = rolePermissionService;
        }


        // GET: api/values
        [HttpGet("{roleId}")]
        public CommonApiResponse<dynamic> Get(Guid roleId)
        {
            jwt = ViewBag.Jwt;

            var result = _rolePermissionService.GetByUserIdWithJoinPermission(jwt.UserId, roleId);
            return CommonApiResponse<dynamic>.Create(Response, System.Net.HttpStatusCode.OK, true, result, null);
        }

        // POST api/values
        [HttpPost("{roleId}/{_permissionId}")]
        public CommonApiResponse<dynamic> Post(Guid roleId, string _permissionId)
        {
            jwt = ViewBag.Jwt;
            Guid permissionId = Guid.Parse(_permissionId);
            RolePermission rolePermission = new RolePermission();
            rolePermission.UserId = jwt.UserId;
            rolePermission.PermissionId = permissionId;
            rolePermission.RoleId = roleId;

            bool result = false;
            Guid insertId;
            string error = "";


            try
            {
                insertId = _rolePermissionService.Save(rolePermission);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            result = Guid.TryParse(insertId.ToString(), out insertId);
            if (result)
            {
                rolePermission.Id = insertId;
                var result1 = _rolePermissionService.GetByUserIdAndIdWithJoinPermission(jwt.UserId, roleId, rolePermission.Id);

                return CommonApiResponse<dynamic>.Create(Response, System.Net.HttpStatusCode.OK, true, result1, null);
            }

            return CommonApiResponse<dynamic>.Create(Response, System.Net.HttpStatusCode.BadRequest, false, null, FluentValidationHelper.GenerateErrorList(error));
        }


        // DELETE api/values/5
        [HttpDelete("{rolePermissionId}")]
        public CommonApiResponse<string> Delete(Guid rolePermissionId)
        {
            jwt = ViewBag.Jwt;
            bool result = _rolePermissionService.Delete(jwt.UserId, rolePermissionId);

            if (result)
            { return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başarılı", null); }

            return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
        }
    }
}
