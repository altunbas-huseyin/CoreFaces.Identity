

using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Models.Models.Roles;
using CoreFaces.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Services
{
    public interface IRoleService : IBaseService<Role>
    {
        Role GetByEmail(string email);
        Role GetByName(Guid userId, string name);
        List<RoleView> GetByUserId(Guid userId);
        Role GetById(Guid userId, Guid id);
        Role GetByUserIdAndName(Guid userId, string name);
        bool Delete(Guid id, Guid userId);
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionService _rolePermissionService;
        public RoleService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _roleRepository = new RoleRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
            _rolePermissionService = new RolePermissionService(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public Role GetByEmail(string email)
        {
            return _roleRepository.GetByEmail(email);
        }

        public Role GetById(Guid id)
        {
            return _roleRepository.GetById(id);
        }

        public Guid Save(Role test)
        {
            _roleRepository.Save(test);
            return test.Id;
        }

        public bool Delete(Guid id)
        {
            return _roleRepository.Delete(id);
        }

        public bool Delete(Guid id, Guid userId)
        {
            return _roleRepository.Delete(id, userId);
        }

        public bool Update(Role test)
        {
            return _roleRepository.Update(test);
        }

        public Role GetByName(Guid userId, string name)
        {
            return _roleRepository.GetByName(userId, name);
        }

        public List<RoleView> GetByUserId(Guid userId)
        {
            List<RoleView> roleViewList = new List<RoleView>();
            List<Role> roleList = _roleRepository.GetByUserId(userId);
            foreach (Role role in roleList)
            {
                roleViewList.Add(this.RoleToRoleView(role));
            }
            List<RolePermission> rolePermissionList = _rolePermissionService.GetByUserId(userId);

            for (int i = 0; i < roleViewList.Count; i++)
            {
                RoleView roleView = roleViewList[i];
                roleView.RolePermissionList = new List<RolePermission>();
                roleView.RolePermissionList = rolePermissionList.FindAll(p => p.RoleId == roleView.Id);
            }
            return roleViewList;
        }

        public RoleView RoleToRoleView(Role role)
        {
            RoleView roleView = new RoleView();
            roleView.Id = role.Id;
            roleView.UserId = role.UserId;
            try
            {
                roleView.Name = role.Name;
            }
            catch (Exception)
            { }
            try
            {
                roleView.Description = role.Description;
            }
            catch (Exception)
            { }
            roleView.StatusId = role.StatusId;
            roleView.CreateDate = role.CreateDate;
            roleView.UpdateDate = role.UpdateDate;
            return roleView;
        }

        public Role GetById(Guid userId, Guid id)
        {
            return _roleRepository.GetById(userId, id);
        }

        public Role GetByUserIdAndName(Guid userId, string name)
        {
            return _roleRepository.GetByUserIdAndName(userId, name);
        }

        public List<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }
    }
}
