

using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Repositories;
using System;
using System.Collections.Generic;
using CoreFaces.Identity.Helper;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Identity.Models;
using CoreFaces.Identity.Models.UserRole;
using Microsoft.Extensions.Options;
using CoreFaces.Identity.Models.Models;
using Microsoft.AspNetCore.Http;

namespace CoreFaces.Identity.Services
{
    public interface IUserService : IBaseService<User>
    {
        User GetByEmail(string email);
        UserView LoginByEmail(String email, string password);
        List<User> GetByParentId(Guid parentId);
        List<UserView> GetUserViewByParentId(Guid parentId);
        User GetByParentIdAndId(Guid parentId, Guid id);
        User GetByEmailAndParentId(Guid parentId, String email);
        User GetByEmail(Guid parentId, string email);
        UserView GetUserViewById(Guid parentId, Guid userId);
        UserView GetUserViewById(Guid userId);
        User GetById(Guid parentId, Guid userId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        //private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        public UserService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _userRepository = new UserRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            _jwtService = new JwtService(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            //_roleService = new RoleService(identityDatabaseContext);
            _userRoleService = new UserRoleService(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public Guid Save(User user)
        {
            user.Password = Encripty.EncryptString(user.Password);
            _userRepository.Save(user);
            return user.Id;
        }

        public bool Delete(Guid id)
        {
            return _userRepository.Delete(id);
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);

        }

        public UserView UserToUserView(User user)
        {
            UserView userView = new UserView();
            userView.Email = user.Email;
            userView.Extra1 = user.Extra1;
            userView.Extra2 = user.Extra2;
            userView.Id = user.Id;
            userView.Name = user.Name;
            userView.SurName = user.SurName;
            userView.UpdateDate = user.UpdateDate;
            userView.ParentId = user.ParentId;
            userView.StatusId = user.StatusId;
            userView.FirmName = user.FirmName;
            userView.Title = user.Title;
            userView.SectionName = user.SectionName;
            userView.CustomerId = user.CustomerId;
            userView.AuthorityId = user.AuthorityId;
            userView.IdentityNumber = user.IdentityNumber;
            userView.BirthDate = user.BirthDate;
            userView.Gsm = user.Gsm;
            userView.CreateDate = user.CreateDate;
            userView.StartDate = user.StartDate;
            userView.EndDate = user.EndDate;
            return userView;
        }

        public UserView LoginByEmail(string email, string password)
        {
            UserView userView = null;
            password = Encripty.EncryptString(password);
            User user = _userRepository.LoginByEmail(email, password);

            if (user != null)
            {
                userView = UserToUserView(user);
                Jwt jwt = new Jwt { UserId = user.Id, Token = Guid.NewGuid(), DeadLine = DateTime.Now.AddDays(1) };

                Guid insertId = _jwtService.Save(jwt);
                jwt.Id = insertId;
                userView.Jwt = jwt;
                userView.Roles = _userRoleService.GetByUserId(user.Id);
            }

            return userView;
        }

        public List<User> GetByParentId(Guid parentId)
        {
            return _userRepository.GetByParentId(parentId);
        }

        public User GetByParentIdAndId(Guid parentId, Guid id)
        {
            return _userRepository.GetByParentIdAndId(parentId, id);
        }

        public User GetByEmailAndParentId(Guid parentId, string email)
        {
            return _userRepository.GetByEmailAndParentId(parentId, email);
        }

        public User GetByEmail(Guid parentId, string email)
        {
            return _userRepository.GetByEmail(parentId, email);
        }

        public UserView GetUserViewById(Guid parentId, Guid userId)
        {
            User user = _userRepository.GetById(parentId, userId);
            UserView userView = this.UserToUserView(user);
            userView.Roles = new List<UserRoleView>();
            userView.Roles = _userRoleService.GetByUserId(userId);
            return userView;
        }

        public UserView GetUserViewById(Guid userId)
        {
            User user = _userRepository.GetById(userId);
            UserView userView = this.UserToUserView(user);
            userView.Roles = new List<UserRoleView>();
            userView.Roles = _userRoleService.GetByUserId(userId);
            return userView;
        }

        public User GetById(Guid parentId, Guid userId)
        {
            User user = _userRepository.GetById(parentId, userId);
            return user;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public List<UserView> GetUserViewByParentId(Guid parentId)
        {
            List<User> users = _userRepository.GetByParentId(parentId);
            List<UserView> usersView = new List<UserView>();
            foreach (User user in users)
            {
                usersView.Add(UserToUserView(user));
            }
            return usersView;
        }
    }
}
