using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerqAccess.SampleQuery
{
    public interface SQLMap
    {
        // register your SQL statements like below
        string GetAllPeople { get; set; }
        string InsertPerson { get; set; }
    }
}
