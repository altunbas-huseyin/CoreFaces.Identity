using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Filters;
using Microsoft.Extensions.Options;
using CoreFaces.Identity.Models.Models;

namespace CoreFaces.Identity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IOptions<IdentitySettings> _identitySettings;
        public ValuesController(IOptions<IdentitySettings> identitySettings, IdentityDatabaseContext identityDatabaseContext)
        {
            _identitySettings = identitySettings;
            _identityDatabaseContext = identityDatabaseContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
           // int Result = _testService.Delete(Guid.Parse("5531af1c-c06b-4659-ab6a-45cc4b7618b3"));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
