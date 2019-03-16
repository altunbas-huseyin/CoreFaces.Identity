using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoreFaces.Identity.UnitTest
{
    public class BaseTest
    {
        public IdentityDatabaseContext _identityDatabaseContext;
        public readonly IUserService _userService;
        public readonly IRoleService _roleService;
        public readonly IIdentitySchemaService schemaService;

        public readonly IJwtService _jwtService;
        public readonly IPermissionService _permissionService;
        public readonly IRolePermissionService _rolePermissionService;
        public readonly IUserRoleService _userRoleService;

        public BaseTest()
        {
           // var serviceProvider = new ServiceCollection()
           ////.AddEntityFrameworkSqlServer()
           //.AddEntityFrameworkNpgsql()
           ////.AddTransient<ITestService, TestService>()
           //.BuildServiceProvider();

            DbContextOptionsBuilder<IdentityDatabaseContext> builder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
            var connectionString = "server=localhost;userid=root;password=12345;database=Identity;";
            builder.UseMySql(connectionString);
            //.UseInternalServiceProvider(serviceProvider); //burası postgress ile sıkıntı çıkartmıyor, fakat mysql'de çalışmıyor test esnasında hata veriyor.

            _identityDatabaseContext = new IdentityDatabaseContext(builder.Options);
            //_context.Database.Migrate();

            IdentitySettings _identitySettings = new IdentitySettings() { FileUploadFolderPath = "c:/" };
            IOptions<IdentitySettings> options = Options.Create(_identitySettings);
            IHttpContextAccessor iHttpContextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };

            _userService = new UserService(_identityDatabaseContext, options, iHttpContextAccessor);
            _roleService = new RoleService(_identityDatabaseContext, options, iHttpContextAccessor);
            _jwtService = new JwtService(_identityDatabaseContext, options, iHttpContextAccessor);
            _permissionService = new PermissionService(_identityDatabaseContext, options, iHttpContextAccessor);
            _rolePermissionService = new RolePermissionService(_identityDatabaseContext, options, iHttpContextAccessor);
            _userRoleService = new UserRoleService(_identityDatabaseContext, options, iHttpContextAccessor);
            schemaService = new SchemaService(_identityDatabaseContext, options, iHttpContextAccessor);


            //Status Services.
            //StatusSettings _statusSettings = new StatusSettings() { FileUploadFolderPath = "c:/" };
            //IOptions<StatusSettings> statusOptions = Options.Create(_statusSettings);
            //_statusService = new StatusService(_statusDatabaseContext, statusOptions, iHttpContextAccessor);
        }
    }
}
