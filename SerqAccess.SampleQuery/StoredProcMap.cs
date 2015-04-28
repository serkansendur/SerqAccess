using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerqAccess.EasyData
{
    public interface StoredProcMap
    {
        // register your stored procedure names like below
         string GetAllPeople{get;}
         string InsertPerson{get;}

    }
}
