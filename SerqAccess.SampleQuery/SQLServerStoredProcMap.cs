using SerqAccess.EasyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerqAccess.SampleQuery
{
    public class SQLServerStoredProcMap : StoredProcMap
    {
        public string GetAllPeople
        {
            get { return "proc_SelectAllPeople"; }
        }

        public string InsertPerson
        {
            get { return "proc_InsertPerson"; }
        }
    }
}
