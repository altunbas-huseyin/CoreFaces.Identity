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
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Permission GetByName(Guid userId, String name);
        List<Permission> GetByUserId(Guid userId);
        bool Delete(Guid userId, Guid id);
        Permission GetById(Guid userId, Guid id);

    }
    public class PermissionRepository : Licence, IPermissionRepository
    {

        private readonly IdentityDatabaseContext _identityDatabaseContext;

        public PermissionRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
        }

        public int Delete(Guid userId, Guid id)
        {
            Permission user = this.GetById(userId, id);
            _identityDatabaseContext.Remove(user);
            int result = _identityDatabaseContext.SaveChanges();
            return result;
        }

        public Guid Save(Permission status)
        {
            _identityDatabaseContext.Add(status);
            _identityDatabaseContext.SaveChanges();

            return status.Id;
        }

        public bool Update(Permission status)
        {
            _identityDatabaseContext.Update(status);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<Permission> GetAll()
        {
            return _identityDatabaseContext.Set<Permission>().OrderBy(p => p.CreateDate).OrderByDescending(p => p.CreateDate).ToList();
        }

        public Permission GetByName(Guid userId, String name)
        {
            Permission permission = _identityDatabaseContext.Set<Permission>().Where(p => p.UserId == userId && p.Name == name).FirstOrDefault();
            return permission;
        }

        public Permission GetById(Guid userId, Guid id)
        {
            Permission permission = _identityDatabaseContext.Set<Permission>().Where(p => p.UserId == userId && p.Id == id).FirstOrDefault();
            return permission;
        }

        public List<Permission> GetByUserId(Guid userId)
        {
            List<Permission> permission = _identityDatabaseContext.Set<Permission>().Where(p => p.UserId == userId).OrderByDescending(p => p.CreateDate).ToList();
            return permission;
        }

        public Permission GetById(Guid id)
        {
            Permission permission = _identityDatabaseContext.Set<Permission>().Where(p => p.Id == id).OrderByDescending(p => p.CreateDate).FirstOrDefault();
            return permission;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        bool IPermissionRepository.Delete(Guid userId, Guid id)
        {
            Permission _permission = this.GetById(userId, id);
            if (_permission == null)
            {
                return true;
            }
            _identityDatabaseContext.Remove(_permission);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}
