using CoreFaces.Identity.Models.Models;
using CoreFaces.Licensing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Identity.Repositories
{
    public interface IIdentitySchemaRepository
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class IdentitySchemaRepository : Licence, IIdentitySchemaRepository
    {
        private readonly IdentityDatabaseContext _identityDatabaseContext;

        public IdentitySchemaRepository(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Identity", iHttpContextAccessor, productSettings.Value.IdentityLicenseDomain, productSettings.Value.IdentityLicenseKey)
        {
            _identityDatabaseContext = identityDatabaseContext;
        }

        public bool DropTables()
        {
            int result = _identityDatabaseContext.Database.ExecuteSqlCommand("DROP TABLE Jwt; DROP TABLE RolePermission; DROP TABLE Permission; DROP TABLE UserRole; DROP TABLE User; DROP TABLE Role;");
            if (result == 0)
                return true;
            else
                return false;
        }

        public bool EnsureCreated()
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_identityDatabaseContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
            return true;
        }
    }

}
