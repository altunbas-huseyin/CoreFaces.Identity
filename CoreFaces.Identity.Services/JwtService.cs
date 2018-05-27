using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Repositories;
using System;
using CoreFaces.Identity.Models;
using Microsoft.Extensions.Options;
using CoreFaces.Identity.Models.Models;
using Microsoft.AspNetCore.Http;
using CoreFaces.Helper;
using System.Collections.Generic;

namespace CoreFaces.Identity.Services
{
    public interface IJwtService : IBaseService<Jwt>
    {
        Result<Jwt> CheckToken(Guid token);
        Result<Jwt> GetByUserId(Guid userId);
    }
    public class JwtService : IJwtService
    {
        private readonly IJwtRepository _jwtRepository;
        public JwtService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _jwtRepository = new JwtRepository(identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public Jwt GetById(Guid id)
        {
            return _jwtRepository.GetById(id);
        }

        public Guid Save(Jwt status)
        {
            _jwtRepository.Save(status);
            return status.Id;
        }

        public bool Delete(Guid id)
        {
            return _jwtRepository.Delete(id);
        }

        public bool Update(Jwt status)
        {
            return _jwtRepository.Update(status);

        }

        public Result<Jwt> CheckToken(Guid token)
        {
            return _jwtRepository.CheckToken(token);
        }

        public Result<Jwt> GetByUserId(Guid userId)
        {
            return _jwtRepository.GetByUserId(userId);
        }

        public List<Jwt> GetAll()
        {
            return _jwtRepository.GetAll();
        }
    }
}
