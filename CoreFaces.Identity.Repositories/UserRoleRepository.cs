using CoreFaces.Identity.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using CoreFaces.Identity.Models.Models;
using Microsoft.AspNetCore.Http;
using CoreFaces.Licensing;
using CoreFaces.Helper;

namespace CoreFaces.Identity.Repositories
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        UserRole GetByName(string name);
        UserRole GetById(Guid ownerId, Guid userId, Guid userRoleId);
        List<UserRole> GetByUserId(Guid ownerId, Guid userId);
        List<UserRole> GetByUserId(Guid userId);
        Result<UserRole> UserAddRole(Guid ownerId, Guid userId, Guid roleId);
        Result<UserRole> UserAddRole(Guid userId, Guid roleId);
        bool UserRemoveRole(Guid parentId, Guid userId, Guid roleId);
        bool UserRemoveRole(Guid userId, Guid roleId);
        bool IsAddedRole(User user, Guid roleId);
        List<UserRole> GetByRoleId(Guid roleId);
    }
    public class UserRoleRepository : Licence, IUserRoleRepository
    {

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private Result<UserRole> result = new Result<UserRole>();

        public UserRoleRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _userRepository = new UserRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            _roleRepository = new RoleRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public Result<UserRole> UserAddRole(Guid ownerId, Guid userId, Guid roleId)
        {
            User user = _userRepository.GetById(ownerId, userId);
            if (user == null)
            {
                result.AddError("Üye bulunamadı.");
                return result;
            }

            //Rol kullanıcıya daha önce atanmış ise bir işlem yapılmıyor
            bool resultIsAdded = IsAddedRole(user, roleId);
            if (resultIsAdded)
            {
                result.AddError("Bu rol daha önce eklenmiş.");
                return result;
            }

            Role role = _roleRepository.GetById(ownerId, roleId);

            UserRole userRole = new UserRole();
            userRole.OwnerId = ownerId;
            userRole.UserId = userId;
            userRole.RoleId = role.Id;
            userRole.StatusId = 1;

            _identityDatabaseContext.Add(userRole);
            int saveResult = _identityDatabaseContext.SaveChanges();
            result.Status = Convert.ToBoolean(saveResult);
            result.Data = userRole;
            return result;
        }


        public Result<UserRole> UserAddRole(Guid userId, Guid roleId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                result.AddError("Üye bulunamadı.");
                return result;
            }

            //Rol kullanıcıya daha önce atanmış ise bir işlem yapılmıyor
            bool resultIsAdded = IsAddedRole(user, roleId);
            if (resultIsAdded)
            {
                result.AddError("Bu rol daha önce eklenmiş.");
                return result;
            }

            Role role = _roleRepository.GetById(roleId);

            UserRole userRole = new UserRole();
            userRole.OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            userRole.UserId = userId;
            userRole.RoleId = role.Id;
            userRole.StatusId = 1;

            _identityDatabaseContext.Add(userRole);
            int saveResult = _identityDatabaseContext.SaveChanges();
            result.Status = Convert.ToBoolean(saveResult);
            result.Data = userRole;
            return result;
        }

        public bool UserRemoveRole(Guid parentId, Guid userId, Guid roleId)
        {
            User user = _userRepository.GetById(parentId, userId);
            if (user == null)
            { return false; }

            UserRole userRole = this.GetById(parentId, userId, roleId);
            _identityDatabaseContext.Remove(userRole);
            int result = _identityDatabaseContext.SaveChanges();

            return Convert.ToBoolean(result);
        }

        public bool UserRemoveRole(Guid userId, Guid roleId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            { return false; }

            UserRole userRole = this.GetById(userId, roleId);
            _identityDatabaseContext.Remove(userRole);
            int result = _identityDatabaseContext.SaveChanges();

            return Convert.ToBoolean(result);
        }

        public bool IsAddedRole(User user, Guid roleId)
        {
            UserRole role = _identityDatabaseContext.Set<UserRole>().Where(p => p.Id == roleId).FirstOrDefault();
            if (role != null)
            {
                return true;
            }

            return false;
        }

        public UserRole GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<UserRole> GetAll()
        {
            return _identityDatabaseContext.Set<UserRole>().ToList();
        }

        public UserRole GetById(Guid ownerId, Guid userId, Guid userRoleId)
        {
            UserRole model = _identityDatabaseContext.Set<UserRole>().Where(p => p.UserId == userId && p.Id == userRoleId && p.OwnerId == ownerId).FirstOrDefault();
            return model;
        }

        public UserRole GetById(Guid userId, Guid userRoleId)
        {
            UserRole model = _identityDatabaseContext.Set<UserRole>().Where(p => p.UserId == userId && p.Id == userRoleId).FirstOrDefault();
            return model;
        }

        public Guid Save(UserRole userRole)
        {
            _identityDatabaseContext.Add(userRole);
            _identityDatabaseContext.SaveChanges();

            return userRole.Id;
        }

        public bool Update(UserRole userRole)
        {
            _identityDatabaseContext.Update(userRole);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserRole GetById(Guid id)
        {
            UserRole model = _identityDatabaseContext.Set<UserRole>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public List<UserRole> GetByUserId(Guid ownerId, Guid userId)
        {
            List<UserRole> model = _identityDatabaseContext.Set<UserRole>().Where(p => p.OwnerId == ownerId && p.UserId == userId).OrderByDescending(p => p.CreateDate).ToList();
            return model;
        }

        public List<UserRole> GetByUserId(Guid userId)
        {
            List<UserRole> model = _identityDatabaseContext.Set<UserRole>().Where(p => p.UserId == userId).OrderByDescending(p => p.CreateDate).ToList();
            return model;
        }

        public List<UserRole> GetByRoleId(Guid roleId)
        {
            List<UserRole> model = _identityDatabaseContext.Set<UserRole>().Where(p => p.RoleId == roleId).OrderByDescending(p => p.CreateDate).ToList();
            return model;
        }
    }
}
