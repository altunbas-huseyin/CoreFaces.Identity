using System;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Models;
using CoreFaces.Helper;

namespace CoreFaces.Identity.PublicControllers.ControllersV1
{
    [Route("api/[controller]")]
    public class JwtController : BaseController
    {
        private readonly IJwtService _iJwtService;
        private readonly IdentityDatabaseContext _identityDatabaseContext;
        public JwtController(IJwtService iJwtService, IdentityDatabaseContext identityDatabaseContext)
        {
            _iJwtService = iJwtService;
            _identityDatabaseContext = identityDatabaseContext;
        }



        [HttpPost("{token}")]
        [Route("/api/jwt/CheckToken/")]
        public CommonApiResponse<Jwt> CheckToken(Guid token)
        {
            CoreFaces.Helper.Result<Jwt> jwt = _iJwtService.CheckToken(token);
            if (jwt.Status)
            {
                return CommonApiResponse<Jwt>.Create(Response, System.Net.HttpStatusCode.OK, true, jwt.Data, null);
            }
            else
            {
                return CommonApiResponse<Jwt>.Create(Response, System.Net.HttpStatusCode.OK, false, null, "Jwt not found.");
            }
        }
    }
}
