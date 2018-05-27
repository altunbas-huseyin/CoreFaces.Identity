using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models.Roles;
using CoreFaces.Identity.Models.UserRole;
using CoreFaces.Identity.Models.Users;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFaces.Identity.Client
{
    public class Client
    {

        public Guid Token { get; set; }
        public string _loginServiceUrl { get; set; }
        IMemoryCache memoryCache;
        public Client(string loginServiceUrl)
        {
            var provider = new ServiceCollection()
                       .AddMemoryCache()
                       .BuildServiceProvider();

            //And now?
            memoryCache = provider.GetService<IMemoryCache>();
            _loginServiceUrl = loginServiceUrl;
        }
        public async Task<UserView> LoginAsync(UserLoginView userLoginView)
        {
            UserView userView = null;
            var client = new RestClient(_loginServiceUrl);
            var request = new RestRequest("api/login", Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-version", "1");
            request.AddParameter("application/json", JsonConvert.SerializeObject(userLoginView).ToString(), ParameterType.RequestBody);
            //client.ExecuteAsync(request, response =>
            //{
            //    var result = response;
            //});
            var response = await client.ExecuteAsync<object>(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            if ((bool)data.status)
            {
                string tempJson = JsonConvert.SerializeObject(data.result);
                userView = JsonConvert.DeserializeObject<UserView>(tempJson);
                Token = userView.Jwt.Token;
            }

            return userView;
        }
        public async Task<Jwt> CheckTokenAsync(Guid token)
        {
            Jwt jwt = null;
            var client = new RestClient(_loginServiceUrl);
            var request = new RestRequest("api/jwt/CheckToken/?token=" + token.ToString(), Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-version", "1");
            //request.AddParameter(token.ToString(), ParameterType.RequestBody);

            var response = await client.ExecuteAsync<object>(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            if ((bool)data.status)
            {
                string tempJson = JsonConvert.SerializeObject(data.result);
                jwt = JsonConvert.DeserializeObject<Jwt>(tempJson);
            }
            return jwt;
        }
        public async Task<UserView> GetUserByTokenAsync(Guid token)
        {
            UserView user = null;
            var client = new RestClient(_loginServiceUrl);
            var request = new RestRequest("api/AppUser/Users/", Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-version", "1");
            request.AddHeader("token", token.ToString());

            var response = await client.ExecuteAsync<object>(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            if ((bool)data.status)
            {
                string tempJson = JsonConvert.SerializeObject(data.result);
                user = JsonConvert.DeserializeObject<UserView>(tempJson);
            }
            return user;
        }
        public async Task<UserView> GetSystemUserAsync(UserLoginView userLoginView)
        {
            UserView SystemUser = null;
            SystemUser = await this.LoginAsync(userLoginView);
            return SystemUser;
        }
        public async Task<UserView> GetSystemUserCacheAsync(UserLoginView userLoginView)
        {
            UserView SystemUser = null;
            memoryCache.TryGetValue<UserView>("SystemUser", out SystemUser);
            if (SystemUser != null)
            {
                return SystemUser;
            }

            SystemUser = await GetSystemUserAsync(userLoginView);

            MemoryCacheEntryOptions cacheOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(10) - DateTime.Now)
            };
            memoryCache.Set("SystemUser", SystemUser, cacheOption);
            return SystemUser;
        }
        public async Task<List<RoleView>> GetRoleListAsync(Guid token)
        {
            List<RoleView> roleView = null;
            var client = new RestClient(_loginServiceUrl);
            var request = new RestRequest("api/SystemAdmin/Roles/", Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-version", "1");
            request.AddHeader("Token", token.ToString());

            var response = await client.ExecuteAsync<object>(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            if ((bool)data.status)
            {
                string tempJson = JsonConvert.SerializeObject(data.result);
                roleView = JsonConvert.DeserializeObject<List<RoleView>>(tempJson);
            }

            return roleView;
        }
        public async Task<List<RoleView>> GetRoleListCacheAsync(Guid token)
        {
            List<RoleView> roleView = null;
            memoryCache.TryGetValue<List<RoleView>>("SystemRoleList", out roleView);
            if (roleView != null && roleView.Count > 0)
            { return roleView; }

            roleView = await GetRoleListAsync(token);

            MemoryCacheEntryOptions cacheOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddDays(200) - DateTime.Now)
            };

            memoryCache.Set("SystemRoleList", roleView, cacheOption);
            return roleView;
        }
        public async Task<List<Permission>> GetPermissionListAsync(Guid token)
        {
            List<Permission> permissionList = null;
            var client = new RestClient(_loginServiceUrl);
            var request = new RestRequest("api/SystemAdmin/Permissions/", Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-version", "1");
            request.AddHeader("Token", token.ToString());

            var response = await client.ExecuteAsync<object>(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            if ((bool)data.status)
            {
                string tempJson = JsonConvert.SerializeObject(data.result);
                permissionList = JsonConvert.DeserializeObject<List<Permission>>(tempJson);
            }

            return permissionList;
        }
        public async Task<List<Permission>> GetPermissionListCacheAsync(Guid token)
        {
            List<Permission> permissionList = null;
            memoryCache.TryGetValue<List<Permission>>("PermissionList", out permissionList);
            if (permissionList != null && permissionList.Count > 0)
            {
                return permissionList;
            }

            permissionList = await GetPermissionListAsync(token);

            MemoryCacheEntryOptions cacheOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddDays(200) - DateTime.Now)
            };
            memoryCache.Set("PermissionList", permissionList, cacheOption);

            return permissionList;
        }
        public static bool IsAccessRole(List<RoleView> systemRoleList, List<Permission> permissionList, List<UserRoleView> userRoleViewList, string requestControllerAndMethodName)
        {
            bool isAccess = false;
            foreach (UserRoleView userRoleView in userRoleViewList)
            {
                List<RoleView> searchRoleView = systemRoleList.Where(p => p.Id == userRoleView.RoleId).ToList();
                foreach (RoleView roleView in searchRoleView)
                {
                    foreach (RolePermission rolePermission in roleView.RolePermissionList)
                    {
                        List<Permission> permission = permissionList.Where(p => p.Id == rolePermission.PermissionId).ToList();
                        Permission result = permission.Where(p => p.Name == requestControllerAndMethodName).FirstOrDefault();
                        if (result == null)
                        {
                        }
                        else
                        {
                            isAccess = true;
                        }
                    }
                }
            }
            return isAccess;
        }
    }
}
