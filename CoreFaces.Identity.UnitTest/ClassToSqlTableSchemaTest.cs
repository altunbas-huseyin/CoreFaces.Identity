using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Helper;

namespace CoreFaces.Identity.UnitTest
{
    
    [TestClass]
    public class ClassToSqlTableSchemaTest : BaseTest
    {
     

        [TestMethod]
        public void ClassToSqlTableScript()
        {
            /*
            //�rnek tablo bu �ekilde olu�mal�
            CREATE TABLE public."User"
            (
                "Id" uuid NOT NULL DEFAULT gen_random_uuid()
                CONSTRAINT user_pkey PRIMARY KEY ("Id"),
            )
             */
            Role t = new Role();
            ClassToSqlTableSchema _classToSqlTableSchema = new ClassToSqlTableSchema(new Role().GetType());
            string sql = _classToSqlTableSchema.CreateTableScript();

            _classToSqlTableSchema = new ClassToSqlTableSchema(new Jwt().GetType());
            sql += _classToSqlTableSchema.CreateTableScript();

            _classToSqlTableSchema = new ClassToSqlTableSchema(new Permission().GetType());
            sql += _classToSqlTableSchema.CreateTableScript();

            _classToSqlTableSchema = new ClassToSqlTableSchema(new Role().GetType());
            sql += _classToSqlTableSchema.CreateTableScript();

            _classToSqlTableSchema = new ClassToSqlTableSchema(new RolePermission().GetType());
            sql += _classToSqlTableSchema.CreateTableScript();

            _classToSqlTableSchema = new ClassToSqlTableSchema(new UserRole().GetType());
            sql += _classToSqlTableSchema.CreateTableScript();
        }

       
    }
}
