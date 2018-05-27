using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Controllers;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Services;
using System;

namespace CoreFaces.Identity.UnitTest
{
    
    [TestClass]
    public class JwtServiceTest : BaseTest
    {

        [TestMethod]
        public void AddToken()
        {
            Jwt j = new Jwt();
            j.UserId = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943");
            j.Token = Guid.Parse("fcbe54b8-8798-4d30-b695-8ffb6539911c");
            j.DeadLine = DateTime.Now.AddDays(5);
            _jwtService.Save(j);
        }

        [TestMethod]
        public void GetToken()
        {
            Jwt result = (Jwt)_jwtService.CheckToken(Guid.Parse("fcbe54b8-8798-4d30-b695-8ffb6539911c")).Data;
            Assert.AreNotSame(result, null);
        }


    }
}
