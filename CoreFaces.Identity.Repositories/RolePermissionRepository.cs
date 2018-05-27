using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Licensing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreFaces.Identity.Repositories
{
    public interface IRolePermissionRepository : IBaseRepository<RolePermission>
    {
        RolePermission GetById(Guid userId, Guid id);
        List<RolePermission> GetByUserId(Guid userId);
        List<RolePermission> GetByUserIdAndRoleId(Guid userId, Guid roleId);
        RolePermission GetByUserIdAndPermissionId(Guid userId, Guid rolePermissionId);
        dynamic GetByUserIdWithJoinPermission(Guid userId, Guid roleId);
        dynamic GetByUserIdAndIdWithJoinPermission(Guid userId, Guid roleId, Guid id);
        bool Delete(Guid userId, Guid rolePermissionId);
        List<RolePermission> GetByPermissionId(Guid userId, Guid permissionId);
    }
    public class RolePermissionRepository : Licence, IRolePermissionRepository
    {

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IPermissionRepository _permissionRepository;
        public RolePermissionRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _permissionRepository = new PermissionRepository(_identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public bool Delete(Guid id)
        {
            RolePermission user = this.GetById(id);
            _identityDatabaseContext.Remove(user);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public RolePermission GetById(Guid id)
        {
            RolePermission model = _identityDatabaseContext.Set<RolePermission>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(RolePermission rolePermission)
        {
            _identityDatabaseContext.Add(rolePermission);
            _identityDatabaseContext.SaveChanges();

            return rolePermission.Id;
        }

        public bool Update(RolePermission rolePermission)
        {
            _identityDatabaseContext.Update(rolePermission);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }


        public RolePermission GetById(Guid userId, Guid id)
        {
            RolePermission rolePermission = _identityDatabaseContext.Set<RolePermission>().Where(p => p.UserId == userId && p.Id == id).FirstOrDefault();
            return rolePermission;
        }

        public List<RolePermission> GetByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            List<RolePermission> rolePermissionList = _identityDatabaseContext.Set<RolePermission>().Where(p => p.UserId == userId && p.RoleId == roleId).OrderByDescending(p => p.CreateDate).ToList();
            return rolePermissionList;
        }

        public RolePermission GetByUserIdAndPermissionId(Guid userId, Guid rolePermissionId)
        {
            RolePermission rolePermission = _identityDatabaseContext.Set<RolePermission>().Where(p => p.UserId == userId && p.PermissionId == rolePermissionId).FirstOrDefault();
            return rolePermission;
        }

        public dynamic GetByUserIdWithJoinPermission(Guid userId, Guid roleId)
        {

            List<Permission> permissionList = _permissionRepository.GetByUserId(userId);
            List<RolePermission> rolePermissionList = this.GetByUserIdAndRoleId(userId, roleId);

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                         on rolePermission.PermissionId equals permission.Id
                         select new
                         {
                             rolePermission.Id,
                             rolePermission.PermissionId,
                             rolePermission.RoleId,
                             permission.Name,
                             permission.Description
                         };

            return result;
        }

        public dynamic GetByUserIdAndIdWithJoinPermission(Guid userId, Guid roleId, Guid id)
        {
            List<Permission> permissionList = _permissionRepository.GetByUserId(userId);
            List<RolePermission> rolePermissionList = new List<RolePermission>();
            rolePermissionList.Add(this.GetById(userId, id));

            var result = from rolePermission in rolePermissionList
                         join permission in permissionList
                         on rolePermission.PermissionId equals permission.Id
                         select new
                         {
                             rolePermission.Id,
                             rolePermission.PermissionId,
                             rolePermission.RoleId,
                             permission.Name,
                             permission.Description
                         };

            return result;
        }

        public List<RolePermission> GetByPermissionId(Guid userId, Guid permissionId)
        {
            List<RolePermission> model = _identityDatabaseContext.Set<RolePermission>().Where(p => p.UserId == userId && p.PermissionId == permissionId).OrderByDescending(p => p.CreateDate).ToList();
            return model;
        }

        public bool Delete(Guid userId, Guid rolePermissionId)
        {
            RolePermission _permission = this.GetById(userId, rolePermissionId);

            _identityDatabaseContext.Remove(_permission);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<RolePermission> GetByUserId(Guid userId)
        {
            List<RolePermission> rolePermissionList = _identityDatabaseContext.Set<RolePermission>().Where(p => p.UserId == userId).ToList();
            return rolePermissionList;
        }

        public List<RolePermission> GetAll()
        {
            return _identityDatabaseContext.Set<RolePermission>().ToList();
        }
    }
}
