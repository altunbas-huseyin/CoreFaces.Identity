using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreFaces.Identity.Helper
{


    public class ClassToSqlTableSchema
    {
        private List<KeyValuePair<String, Type>> _fieldInfo = new List<KeyValuePair<String, Type>>();
        private string _className = String.Empty;

        private Dictionary<Type, String> dataMapper
        {
            get
            {
                // Add the rest of your CLR Types to SQL Types mapping here
                Dictionary<Type, String> dataMapper = new Dictionary<Type, string>();
                dataMapper.Add(typeof(int), "BIGINT");
                dataMapper.Add(typeof(string), "varchar");
                dataMapper.Add(typeof(bool), "BIT");
                dataMapper.Add(typeof(DateTime), "timestamp");
                dataMapper.Add(typeof(float), "DECIMAL");
                dataMapper.Add(typeof(decimal), "DECIMAL");
                dataMapper.Add(typeof(Guid), "uuid ");

                return dataMapper;
            }
        }

        public List<KeyValuePair<String, Type>> Fields
        {
            get { return this._fieldInfo; }
            set { this._fieldInfo = value; }
        }

        public string ClassName
        {
            get { return this._className; }
            set { this._className = value; }
        }

        public ClassToSqlTableSchema(Type t)
        {
            this._className = t.Name;

            foreach (PropertyInfo p in t.GetProperties())
            {
                KeyValuePair<String, Type> field = new KeyValuePair<String, Type>(p.Name, p.PropertyType);

                this.Fields.Add(field);
            }
        }

        public string CreateTableScript()
        {
            System.Text.StringBuilder script = new StringBuilder();

            script.AppendLine("CREATE TABLE \"" + this.ClassName + "\"");
            script.AppendLine("(");
            // script.AppendLine("\t ID BIGINT,");
            for (int i = 0; i < this.Fields.Count; i++)
            {
                KeyValuePair<String, Type> field = this.Fields[i];

                if (dataMapper.ContainsKey(field.Value))
                {
                    script.Append("\t \"" + field.Key + "\" " + dataMapper[field.Value]);
                }
                else
                {
                    // Complex Type? 
                    script.Append("\t \"" + field.Key + "\"  varchar");
                }
                if (field.Key == "Id")
                {
                    script.Append(" NOT NULL DEFAULT gen_random_uuid()");
                }

                script.Append(",");


                script.Append(Environment.NewLine);
            }
           
            script.Append(Environment.NewLine);
            script.Append("CONSTRAINT " + this.ClassName + "_pkey PRIMARY KEY (\"Id\")");
            script.AppendLine(")");

            return script.ToString();
        }
    }

}
