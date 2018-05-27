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

namespace CoreFaces.Identity.AppUser.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/AppUser/[controller]")]
    [ValidateModel()]
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
        public CommonApiResponse<UserView> Get()
        {
            jwt = ViewBag.Jwt;

            Jwt jwtResult = _iJwtService.CheckToken(jwt.Token).Data;
            if (jwtResult == null)
            {
                return CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.OK, true, null, "Token invalid.");
            }

            UserView user = _userService.GetUserViewById(jwt.UserId);
            return CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
        }


    }
}
