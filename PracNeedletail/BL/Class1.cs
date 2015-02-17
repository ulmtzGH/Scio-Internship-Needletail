using DataBD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Class1
    {
        public static void CreateAddress()
        {
            Data_DB.CreateAddress();
        }

        public static void CreateDepartment()
        {
            Data_DB.CreateDepartment();
        }

        public static void DeleteAddress(string streetId)
        {
            Data_DB.DeleteAddress(streetId);
        }

        public static void DeleteDepartment(string departnameId)
        {
            Data_DB.DeleteDepartment(departnameId);
        }

        public static void GetAddress(Guid addrId)
        {
            Data_DB.GetAddress(addrId);
        }

        public static void GetDepartment(Guid departId)
        {
            Data_DB.GetDepartment(departId);
        }

        public static void UpdateAddress(string where)
        {
            Data_DB.UpdateAddress(new {Street="Morelos",ZipCode="67891", Phone="777 333 1234" }, new{Id=where});
        }

        public static void UpdateDepartment(string where)
        {
            Data_DB.UpdateDepartment(new { DepartmentName = "Contabilidad", ParentDepartmentId = Guid.NewGuid() }, new { Id = where });
        }

        public static IEnumerable GetAddressAll()
        {
            return Data_DB.GetAddressAll();
        }

        public static IEnumerable GetDepartmentAll()
        {
            return Data_DB.GetDepartmentAll();
        }
    }
}
