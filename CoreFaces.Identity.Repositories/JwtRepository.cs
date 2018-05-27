using CoreFaces.Identity.Models.Domain;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using CoreFaces.Identity.Models.Models;
using Microsoft.AspNetCore.Http;
using CoreFaces.Licensing;
using CoreFaces.Helper;
using System.Collections.Generic;

namespace CoreFaces.Identity.Repositories
{
    public interface IJwtRepository : IBaseRepository<Jwt>
    {
        Result<Jwt> CheckToken(Guid token);
        Result<Jwt> GetByUserId(Guid userId);
    }
    public class JwtRepository : Licence, IJwtRepository
    {

        private readonly IdentityDatabaseContext _identityDatabaseContext;

        public JwtRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, identitySettings.Value.IdentityLicenseDomain, identitySettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
        }

        public Result<Jwt> CheckToken(Guid token)
        {
            Jwt jwt = null;
            var resultTokens = _identityDatabaseContext.Set<Jwt>().Where(p => p.Token == token).ToList();
            if (resultTokens.Count > 0)
            {
                jwt = resultTokens[0];

                if (jwt.DeadLine < DateTime.Now)
                {
                    jwt = null;
                }
            }
            if (jwt == null)
            { return new Result<Jwt>(jwt, false); }
            else
            { return new Result<Jwt>(jwt, true); }
        }

        public bool Delete(Guid id)
        {
            Jwt jwt = this.GetById(id);
            _identityDatabaseContext.Remove(jwt);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<Jwt> GetAll()
        {
            return _identityDatabaseContext.Set<Jwt>().ToList();
        }

        public Jwt GetById(Guid id)
        {
            Jwt model = _identityDatabaseContext.Set<Jwt>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Result<Jwt> GetByUserId(Guid userId)
        {
            Jwt jwt = _identityDatabaseContext.Set<Jwt>().Where(p => p.UserId == userId).FirstOrDefault();
            return new Result<Jwt>(jwt);
        }

        public Guid Save(Jwt jwt)
        {
            _identityDatabaseContext.Add(jwt);
            _identityDatabaseContext.SaveChanges();

            return jwt.Id;
        }

        public bool Update(Jwt jwt)
        {
            _identityDatabaseContext.Update(jwt);
            int result = _identityDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

     
    }
}
