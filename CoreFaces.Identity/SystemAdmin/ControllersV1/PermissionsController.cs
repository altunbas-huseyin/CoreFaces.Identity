using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Helper;
using CoreFaces.Identity.Filters;
using CoreFaces.Identity.Models.Permissions;
using CoreFaces.Identity.Models;
using CoreFaces.Helper;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFaces.Identity.SystemAdmin.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/SystemAdmin/[controller]")]
    [ValidateModel("1c823a7d-7475-4c09-ad13-3b94a53ca943,57daa98a-3c56-4f0e-9247-3a07ac1b4c08")]
    public class PermissionsController : BaseController
    {

        Jwt jwt = new Jwt();

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IPermissionService _permissionService;

        public PermissionsController(IdentityDatabaseContext identityDatabaseContext, IPermissionService permissionService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _permissionService = permissionService;
        }

        // GET: api/values
        [HttpGet]
        public CommonApiResponse<List<Permission>> Get()
        {
            jwt = ViewBag.Jwt;
            List<Permission> list = _permissionService.GetByUserId(jwt.UserId);
            return CommonApiResponse<List<Permission>>.Create(Response, System.Net.HttpStatusCode.OK, true, list, null);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse<Permission> Get(Guid Id)
        {
            jwt = ViewBag.Jwt;
            Permission permission = _permissionService.GetById(jwt.UserId, Id);
            return CommonApiResponse<Permission>.Create(Response, System.Net.HttpStatusCode.OK, true, permission, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse<Permission> Post(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            Permission permission = new Permission();
            permission.UserId = jwt.UserId;
            permission.Name = permissionView.Name;
            permission.Description = permissionView.Description;

            Guid insertId = _permissionService.Save(permission);
            bool result = Guid.TryParse(insertId.ToString(), out insertId);
            if (result)
            { return CommonApiResponse<Permission>.Create(Response, System.Net.HttpStatusCode.OK, true, permission, null); }

            return CommonApiResponse<Permission>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse<Permission> Put(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            Permission permission = new Permission();
            permission.Id = permissionView.Id;
            permission.UserId = jwt.UserId;
            permission.Name = permissionView.Name;
            permission.Description = permissionView.Description;

            bool result = _permissionService.Update(permission);
            if (result)
            { return CommonApiResponse<Permission>.Create(Response, System.Net.HttpStatusCode.OK, true, permission, null); }

            return CommonApiResponse<Permission>.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("An error occurred."));
        }

        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse<string> Delete(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            CoreFaces.Helper.Result<Permission> result = _permissionService.Delete(jwt.UserId, permissionView.Id);
            Permission p = result.Data;
            if (result.Status)
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.OK, true, "Success.", result.ErrorList);
            }
            else
            {
                return CommonApiResponse<string>.Create(Response, System.Net.HttpStatusCode.Conflict, false, null, result.ErrorList);
            }
        }


    }
}
