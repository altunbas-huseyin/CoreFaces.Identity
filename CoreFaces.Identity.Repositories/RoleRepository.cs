using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Models.Models.Roles;
using CoreFaces.Licensing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreFaces.Identity.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role GetByEmail(string email);
        Role GetByName(Guid userId, string name);
        List<Role> GetByUserId(Guid userId);
        Role GetById(Guid userId, Guid id);
        Role GetByUserIdAndName(Guid userId, string name);
        bool Delete(Guid id, Guid userId);
    }
    public class RoleRepository : Licence, IRoleRepository
    {
        private readonly IdentityDatabaseContext _identityDatabaseContext;

        public RoleRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
        }

        public Role GetByEmail(string email)
        {
            Guid g = Guid.Parse("dfe32bbd-3e0e-4c79-ab17-5f3104296b7e");
            Role model = _identityDatabaseContext.Set<Role>().Where(p => p.Id == g).FirstOrDefault();
            return model;
        }

        public Role GetById(Guid id)
        {
            Role model = _identityDatabaseContext.Set<Role>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(Role role)
        {
            _identityDatabaseContext.Add(role);
            _identityDatabaseContext.SaveChanges();
            return role.Id;
        }


        public bool Delete(Guid id)
        {
            Role role = this.GetById(id);
            _identityDatabaseContext.Remove(role);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Delete(Guid id, Guid userId)
        {
            Role role = GetById(userId, id);
            _identityDatabaseContext.Remove(role);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Role role)
        {
            _identityDatabaseContext.Update(role);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public Role GetByName(Guid userId, string name)
        {
            Role role = _identityDatabaseContext.Set<Role>().Where(p => p.UserId == userId && p.Name == name).FirstOrDefault();
            return role;
        }

        public List<Role> GetByUserId(Guid userId)
        {
            List<Role> roleList = _identityDatabaseContext.Set<Role>().Where(p => p.UserId == userId).OrderByDescending(p => p.CreateDate).ToList();
            return roleList;
        }

        public Role GetById(Guid userId, Guid id)
        {
            Role role = _identityDatabaseContext.Set<Role>().Where(p => p.Id == id).FirstOrDefault();
            return role;
        }

        public Role GetByUserIdAndName(Guid userId, string name)
        {
            Role role = _identityDatabaseContext.Set<Role>().Where(p => p.UserId == userId && p.Name == name).FirstOrDefault();
            return role;
        }

        public List<Role> GetAll()
        {
            return _identityDatabaseContext.Set<Role>().ToList();
        }
    }


}
