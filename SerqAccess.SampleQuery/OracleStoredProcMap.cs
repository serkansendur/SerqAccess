using SerqAccess.EasyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerqAccess.SampleQuery
{
    public class OracleStoredProcMap : StoredProcMap
    {
        public string GetAllPeople
        {
            get { return "PROC_SELECTALLPEOPLE"; }
        }

        public string InsertPerson
        {
            get { return "PROC_INSERTPERSON"; }
        }
    }
}
