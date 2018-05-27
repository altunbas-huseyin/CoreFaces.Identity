using CoreFaces.Identity.Models.Models;
using CoreFaces.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Identity.Services
{
    public interface IIdentitySchemaService
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class SchemaService : IIdentitySchemaService
    {
        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IIdentitySchemaRepository _identitySchemaRepository;
        public SchemaService(IdentityDatabaseContext identityDatabaseContext, IOptions<IdentitySettings> identitySettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _identitySchemaRepository = new IdentitySchemaRepository(_identityDatabaseContext, identitySettings, iHttpContextAccessor);
        }

        public bool DropTables()
        {
            return _identitySchemaRepository.DropTables();
        }

        public bool EnsureCreated()
        {
            return _identitySchemaRepository.EnsureCreated();
        }
    }

}
