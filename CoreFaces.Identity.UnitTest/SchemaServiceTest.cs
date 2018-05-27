using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Controllers;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Services;
using System;

namespace CoreFaces.Identity.UnitTest
{

    [TestClass]
    public class SchemaServiceTest : BaseTest
    {

        [TestMethod]
        public void DropTables()
        {
            //bool result = schemaService.DropTables();
            //Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void EnsureCreated()
        {
            //bool result = schemaService.EnsureCreated();
            //Assert.AreEqual(result, true);
        }

    }
}
