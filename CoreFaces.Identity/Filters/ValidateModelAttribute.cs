using CoreFaces.Identity.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using CoreFaces.Identity.Models.UserRole;
using CoreFaces.Identity.Models;
using CoreFaces.Helper;

namespace CoreFaces.Identity.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute, IActionFilter
    {

        List<string> requiredRoleList = new List<string>();

        Microsoft.Extensions.Primitives.StringValues _Token = "";
        bool IsAcces = false;

        private readonly RequestDelegate _next;



        public ValidateModelAttribute(RequestDelegate next)
        {
            _next = next;

        }
        public ValidateModelAttribute()
        {

        }
        public ValidateModelAttribute(string _role)
        {
            requiredRoleList = _role.Split(new char[] { ',' }).ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IdentityDatabaseContext _identityDatabaseContext = (IdentityDatabaseContext)context.HttpContext.RequestServices.GetService(typeof(IdentityDatabaseContext));
            IJwtService _jwtService = (JwtService)context.HttpContext.RequestServices.GetService(typeof(IJwtService));
            IUserService _userService = (UserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            IRoleService _roleService = (RoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService));
            IUserRoleService _userRoleService = (UserRoleService)context.HttpContext.RequestServices.GetService(typeof(IUserRoleService));

            context.HttpContext.Request.Headers.TryGetValue("Token", out _Token);
            if (_Token.Count > 0)
            {
                Guid token;
                Jwt jwt = new Jwt();
                try
                {
                    token = Guid.Parse(_Token.FirstOrDefault());

                    jwt = (Jwt)_jwtService.CheckToken(token).Data;
                    if (jwt == null)
                    {
                        CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Token geçersiz.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }

                try
                {
                    var controller = context.Controller as Controller;
                    User user = _userService.GetById(jwt.UserId);
                    List<UserRoleView> userRoleViewList = _userRoleService.GetByUserId(jwt.UserId);
                    if (user == null)
                    {
                        CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Kullanıcı bulunamadı.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }

                    if (requiredRoleList.Count > 0)
                    {
                        foreach (string requiredRoleName in requiredRoleList)
                        {
                            foreach (var userRole in userRoleViewList)
                            {
                                if (Guid.Parse(requiredRoleName) == userRole.RoleId)
                                {
                                    IsAcces = true;
                                    break;
                                }
                            }
                        }
                        if (!IsAcces)
                        {

                            CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Yetkiniz yok.");
                            BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                            context.Result = badReq;
                            return;
                        }
                    }

                    controller.ViewBag.Jwt = jwt;
                    controller.ViewBag.User = user;
                }
                catch (Exception ex)
                {
                    CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }
            }
            else
            {
                CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Header Token bulunamadı.");
                ObjectResult badReq = new ObjectResult(response);
                context.Result = badReq;

            }
        }
    }

}
