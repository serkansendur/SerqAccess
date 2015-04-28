using SerqAccess.EasyData;
using SerqAccess.SampleEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerqAccess.EasyData.Extensions;
namespace SerqAccess.SampleQuery
{
    public class PersonQueries
    {
        private DBManagerFactory dbManagerFactory;
        private StoredProcMap storedProcMap;
        private SQLMap sqlMap;
        private PersonQueries(DBManagerFactory dbManagerFactory)
        {
            this.dbManagerFactory = dbManagerFactory;
        }
        public PersonQueries(DBManagerFactory dbManagerFactory, StoredProcMap storedProcMap):this(dbManagerFactory)
        {
            this.storedProcMap = storedProcMap;
        }
        public PersonQueries(DBManagerFactory dbManagerFactory, SQLMap sqlMap):this(dbManagerFactory)
        {
            this.sqlMap = sqlMap;
        }

        public List<Person> GetAll()
        {
            if (storedProcMap != null)
            { return GetAllWithProc(); }
            else if (sqlMap != null)
            { return GetAllWithSQL(); }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private List<Person> GetAllWithSQL()
        {
            var personList = new List<Person>();

            using (var dbManager = dbManagerFactory.GetDBManager())
            {
                dbManager.OpenConnection();

                using (var reader = dbManager.ReadWithSQL(sqlMap.GetAllPeople))
                {
                    while (reader.Read())
                    {
                        var person = new Person
                        {
                            ID = reader.GetValue<decimal>("PKID"),
                            Name = reader.GetValue<string>("Name"),
                            LastName = reader.GetValue<string>("LastName")

                        };
                        personList.Add(person);
                    }
                    reader.Close();
                }
            }

            return personList;
        }

        private List<Person> GetAllWithProc()
        {
            var personList = new List<Person>();

            using (var dbManager = dbManagerFactory.GetDBManager())
            {
                dbManager.OpenConnection();

                using (var reader = dbManager.ReadWithProc(storedProcMap.GetAllPeople))
                {
                    while (reader.Read())
                    {
                        var person = new Person
                        {
                            ID = reader.GetValue<int>("PKID"),
                            Name = reader.GetValue<string>("Name"),
                            LastName = reader.GetValue<string>("LastName")

                        };
                        personList.Add(person);
                    }
                    reader.Close();
                }
            }

            return personList;
        }

        public  int AddPerson(string Name, string LastName)
        {
            if (storedProcMap != null)
            {return AddPersonWithProc(Name, LastName); }
            else if (sqlMap != null)
            { return AddPersonWithSQL(); }
            else
            {
                throw new ArgumentNullException();
            }
           
        }

        private int AddPersonWithSQL()
        {
            int result = 0;
            using (DBManager dbManager = dbManagerFactory.GetDBManager())
            {
                dbManager.OpenConnection();
                result = dbManager.PutWithSQL(sqlMap.InsertPerson);
            }
            return result;
        }

        private int AddPersonWithProc(string Name, string LastName)
        {
            int result = 0;
            List<DBParam> dbParams = new List<DBParam>();

            dbParams.Add(new DBParam(CommonDbType.StringUnicode, "@Name", Name));
            dbParams.Add(new DBParam(CommonDbType.StringUnicode, "@LastName", LastName));

            using (DBManager dbManager = dbManagerFactory.GetDBManager())
            {
                dbManager.OpenConnection();
                result = dbManager.PutWithProc(storedProcMap.InsertPerson, dbParams, false);
            }

            return result;
        }
       
    }
}
