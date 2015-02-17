using Entities;
using Needletail.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBD
{
    public class DB
    {
        public const string ConnectionString = "DefaultConnection";

        static DBTableDataSourceBase<Address, Guid> _Addr = new DBTableDataSourceBase<Address, Guid>(ConnectionString, "Address");
        public static DBTableDataSourceBase<Address, Guid> Addr
        {
            get
            {
                return _Addr;
            }
        }

        static DBTableDataSourceBase<Department, Guid> _Depart = new DBTableDataSourceBase<Department, Guid>(ConnectionString, "Department");
        public static DBTableDataSourceBase<Department, Guid> Depart
        {
            get
            {
                return _Depart;
            }
        }


    }
}
