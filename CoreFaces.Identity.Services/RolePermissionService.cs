

using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Services
{
    public interface IRolePermissionService : IBaseService<RolePermission>
    {
        RolePermission GetById(Guid userId, Guid id);
        List<RolePermission> GetByUserIdAndRoleId(Guid userId, Guid roleId);
        RolePermission GetByUserIdAndPermissionId(Guid userId, Guid rolePermissionId);
        dynamic GetByUserIdWithJoinPermission(Guid userId, Guid roleId);
        dynamic GetByUserIdAndIdWithJoinPermission(Guid userId, Guid roleId, Guid id);
        bool Delete(Guid userId, Guid rolePermissionId);
        List<RolePermission> GetByPermissionId(Guid userId, Guid permissionId);
        List<RolePermission> GetByUserId(Guid userId);
    }
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public RolePermissionService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _rolePermissionRepository = new RolePermissionRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public RolePermission GetById(Guid id)
        {
            return _rolePermissionRepository.GetById(id);
        }

        public Guid Save(RolePermission status)
        {
            _rolePermissionRepository.Save(status);
            return status.Id;
        }

        public bool Delete(Guid id)
        {
            return _rolePermissionRepository.Delete(id);
        }

        public bool Update(RolePermission status)
        {
            return _rolePermissionRepository.Update(status);
        }


        public RolePermission GetById(Guid userId, Guid id)
        {
            return _rolePermissionRepository.GetById(userId, id);
        }

        public List<RolePermission> GetByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            return _rolePermissionRepository.GetByUserIdAndRoleId(userId, roleId);
        }

        public RolePermission GetByUserIdAndPermissionId(Guid userId, Guid rolePermissionId)
        {
            return _rolePermissionRepository.GetByUserIdAndPermissionId(userId, rolePermissionId);
        }

        public dynamic GetByUserIdWithJoinPermission(Guid userId, Guid roleId)
        {
            return _rolePermissionRepository.GetByUserIdWithJoinPermission(userId, roleId);
        }

        public dynamic GetByUserIdAndIdWithJoinPermission(Guid userId, Guid roleId, Guid id)
        {
            return _rolePermissionRepository.GetByUserIdAndIdWithJoinPermission(userId, roleId, id);
        }

        public List<RolePermission> GetByPermissionId(Guid userId, Guid permissionId)
        {
            return _rolePermissionRepository.GetByPermissionId(userId, permissionId);
        }

        public bool Delete(Guid userId, Guid rolePermissionId)
        {
            return _rolePermissionRepository.Delete(userId, rolePermissionId);
        }

        public List<RolePermission> GetByUserId(Guid userId)
        {
            return _rolePermissionRepository.GetByUserId(userId);
        }

        public List<RolePermission> GetAll()
        {
            return _rolePermissionRepository.GetAll();
        }
    }
}
