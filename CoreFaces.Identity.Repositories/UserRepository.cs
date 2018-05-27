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
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
        User LoginByEmail(String email, string password);
        List<User> GetByParentId(Guid parentId);
        User GetByParentIdAndId(Guid parentId, Guid id);
        User GetByEmailAndParentId(Guid parentId, String email);
        User GetByEmail(Guid parentId, string email);
        User GetById(Guid parentId, Guid userId);
    }
    public class UserRepository : Licence, IUserRepository
    {
        private readonly IdentityDatabaseContext _identityDatabaseContext;

        public UserRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
        }

        public User GetByEmail(string email)
        {
            User model = _identityDatabaseContext.Set<User>().Where(p => p.Email == email).FirstOrDefault();
            return model;
        }

        public User GetById(Guid id)
        {
            User model = _identityDatabaseContext.Set<User>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(User user)
        {
            _identityDatabaseContext.Add(user);
            _identityDatabaseContext.SaveChanges();

            //inner join denemesi
            //var fff = from _user in _identityDatabaseContext.Set<User>()
            //          join role in _identityDatabaseContext.Set<Role>() on _user.Id equals role.UserId
            //          // where user.LocationId == 1
            //          select _user;

            return user.Id;
        }


        public bool Delete(Guid id)
        {
            User user = this.GetById(id);
            _identityDatabaseContext.Remove(user);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(User user)
        {
            _identityDatabaseContext.Update(user);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public User LoginByEmail(string email, string password)
        {

            User user = _identityDatabaseContext.Set<User>().Where(p => p.Email == email && p.Password == password).FirstOrDefault();
            return user;
        }

        public List<User> GetByParentId(Guid parentId)
        {
            List<User> userList = _identityDatabaseContext.Set<User>().Where(p => p.ParentId == parentId).OrderByDescending(p => p.CreateDate).ToList();
            return userList;
        }

        public User GetByParentIdAndId(Guid parentId, Guid id)
        {
            User user = _identityDatabaseContext.Set<User>().Where(p => p.ParentId == parentId && p.Id == id).FirstOrDefault();
            return user;
        }

        public User GetByEmailAndParentId(Guid parentId, string email)
        {
            User user = _identityDatabaseContext.Set<User>().Where(p => p.Email == email && p.ParentId == parentId).FirstOrDefault();
            return user;
        }

        public User GetByEmail(Guid parentId, string email)
        {
            User user = _identityDatabaseContext.Set<User>().Where(p => p.Email == email && p.ParentId == parentId).FirstOrDefault();
            return user;
        }

        public User GetById(Guid parentId, Guid userId)
        {
            User model = _identityDatabaseContext.Set<User>().Where(p => p.Id == userId && p.ParentId==parentId).FirstOrDefault();
            return model;
        }

        public List<User> GetAll()
        {
            return _identityDatabaseContext.Set<User>().ToList();
        }
    }


}
