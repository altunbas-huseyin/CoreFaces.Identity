using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using CoreFaces.Identity.Filters;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Helper;

namespace CoreFaces.Identity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration
            services.Configure<IdentitySettings>(Configuration.GetSection("IdentitySettings"));

            // Add framework services.
            services.AddMvc();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            //Cors Policy 
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("*")//AllowOrigins diyerek herkese ya da belirli adreslere erişim hakkı verebilirsiniz.
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            }));

            var connectionString = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<IdentityDatabaseContext>(opts => opts.UseMySql(connectionString));

            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseCors("CorsPolicy");//Cors Policy 
            //app.UseMvc();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
            app.UseResponseWrapper();
        }

       
    }
}
