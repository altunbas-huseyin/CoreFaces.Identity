using CoreFaces.Helper;
using CoreFaces.Identity.Models;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Models.UserRole;
using CoreFaces.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Services
{
    public interface IUserRoleService : IBaseService<UserRole>
    {
        UserRole GetByName(string name);
        List<UserRole> GetAll();
        List<UserRoleView> GetByUserId(Guid ownerId, Guid userId);
        List<UserRoleView> GetByUserId(Guid userId);
        Result<UserRole> UserAddRole(Guid ownerId, Guid userId, Guid roleId);
        Result<UserRole> UserAddRole(Guid userId, Guid roleId);
        bool UserRemoveRole(Guid parentId, Guid userId, Guid roleId);
        bool UserRemoveRole(Guid userId, Guid roleId);
        bool IsAddedRole(User user, Guid roleId);
        UserRoleView UserRoleToUserRoleView(UserRole userRole);
        List<UserRole> GetByRoleId(Guid roleId);
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public UserRoleService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _userRoleRepository = new UserRoleRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            _roleRepository = new RoleRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public UserRole GetById(Guid id)
        {
            return _userRoleRepository.GetById(id);
        }

        public Guid Save(UserRole userRole)
        {
            _userRoleRepository.Save(userRole);
            return userRole.Id;
        }

        public bool Delete(Guid id)
        {
            return _userRoleRepository.Delete(id);
        }

        public bool Update(UserRole userRole)
        {
            return _userRoleRepository.Update(userRole);

        }

        public UserRole GetByName(string name)
        {
            return _userRoleRepository.GetByName(name);
        }

        public List<UserRole> GetAll()
        {
            return _userRoleRepository.GetAll();
        }

        public UserRoleView UserRoleToUserRoleView(UserRole userRole)
        {
            UserRoleView userRoleView = new UserRoleView();
            userRoleView.Id = userRole.Id;
            userRoleView.OwnerId = userRole.OwnerId;
            userRoleView.RoleId = userRole.RoleId;
            userRoleView.StatusId = userRole.StatusId;
            userRoleView.UserId = userRoleView.UserId;
            userRoleView.CreateDate = userRole.CreateDate;
            userRoleView.UpdateDate = userRole.UpdateDate;


            return userRoleView;
        }

        public List<UserRoleView> GetByUserId(Guid ownerId, Guid userId)
        {
            List<UserRole> userRoleList = _userRoleRepository.GetByUserId(ownerId, userId);
            List<UserRoleView> userRoleViewList = new List<UserRoleView>();
            foreach (UserRole userRole in userRoleList)
            {
                Role role = _roleRepository.GetById(userRole.RoleId);
                UserRoleView userRoleView = UserRoleToUserRoleView(userRole);
                userRoleView.Name = role.Name;
                userRoleView.RoleDescription = role.Description;

                userRoleViewList.Add(userRoleView);
            }

            return userRoleViewList;
        }

        public List<UserRoleView> GetByUserId(Guid userId)
        {
            List<UserRole> userRoleList = _userRoleRepository.GetByUserId(userId);
            List<UserRoleView> userRoleViewList = new List<UserRoleView>();
            foreach (UserRole userRole in userRoleList)
            {
                Role role = _roleRepository.GetById(userRole.RoleId);
                UserRoleView userRoleView = UserRoleToUserRoleView(userRole);
                userRoleView.Name = role.Name;

                userRoleViewList.Add(userRoleView);
            }

            return userRoleViewList;
        }

        public Result<UserRole> UserAddRole(Guid ownerId, Guid userId, Guid roleId)
        {
            return _userRoleRepository.UserAddRole(ownerId, userId, roleId);
        }

        public Result<UserRole> UserAddRole(Guid userId, Guid roleId)
        {
            return _userRoleRepository.UserAddRole(userId, roleId);
        }

        public bool UserRemoveRole(Guid parentId, Guid userId, Guid roleId)
        {
            return _userRoleRepository.UserRemoveRole(parentId, userId, roleId);
        }

        public bool UserRemoveRole(Guid userId, Guid roleId)
        {
            return _userRoleRepository.UserRemoveRole(userId, roleId);
        }

        public bool IsAddedRole(User user, Guid roleId)
        {
            return _userRoleRepository.IsAddedRole(user, roleId);
        }

        public List<UserRole> GetByRoleId(Guid roleId)
        {
            return _userRoleRepository.GetByRoleId(roleId);
        }


    }
}
