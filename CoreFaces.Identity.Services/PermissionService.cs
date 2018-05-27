using CoreFaces.Helper;
using CoreFaces.Identity.Models;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;


namespace CoreFaces.Identity.Services
{
    public interface IPermissionService : IBaseService<Permission>
    {
        Permission GetByName(Guid userId, String name);
        List<Permission> GetByUserId(Guid userId);
        List<Permission> GetAll();
        Result<Permission> Delete(Guid userId, Guid id);
        Permission GetById(Guid userId, Guid id);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public PermissionService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _permissionRepository = new PermissionRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            _rolePermissionRepository = new RolePermissionRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public Permission GetById(Guid id)
        {
            return _permissionRepository.GetById(id);
        }

        public Guid Save(Permission status)
        {
            _permissionRepository.Save(status);
            return status.Id;
        }

        public bool Delete(Guid id)
        {
            return _permissionRepository.Delete(id);
        }

        public bool Update(Permission status)
        {
            return _permissionRepository.Update(status);

        }

        public List<Permission> GetAll()
        {
            return _permissionRepository.GetAll();
        }

        public Permission GetByName(Guid userId, string name)
        {
            return _permissionRepository.GetByName(userId, name);
        }

        public List<Permission> GetByUserId(Guid userId)
        {
            return _permissionRepository.GetByUserId(userId);
        }
        public Result<Permission> Delete(Guid userId, Guid id)
        {
            Result<Permission> result = new Result<Permission>();
            List<RolePermission> list = _rolePermissionRepository.GetByPermissionId(userId, id);
            if (list.Count > 0)
            {
                result.ErrorList = CoreFaces.Identity.Helper.FluentValidationHelper.GenerateErrorList("Bu kayıt RolePermission tablosunda kullanıldığı için silinemez.");
            }
            else
            {
                result.Status = _permissionRepository.Delete(userId, id);
            }

            return result;
        }

        public Permission GetById(Guid userId, Guid id)
        {
            return _permissionRepository.GetById(userId, id);
        }
    }
}
