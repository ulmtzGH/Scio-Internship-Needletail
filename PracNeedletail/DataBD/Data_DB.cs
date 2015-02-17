using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBD
{
   public class Data_DB
    {
       public static void CreateAddress()
       {
           Address a = new Address { Id = Guid.NewGuid(), Street = "Zarca", ZipCode = "54321", Phone = "555 222 2222" };
           DB.Addr.Insert(a);
       }

       public static void CreateDepartment()
       {
           Department d = new Department { Id = Guid.NewGuid(), DepartmentName = "Desarrollo", ParentDepartmentId = Guid.NewGuid() };
           DB.Depart.Insert(d);
       }

       public static void DeleteAddress(string streetId)
       {
           DB.Addr.Delete(new { Street = streetId });
       }

       public static void DeleteDepartment(string departnameId)
       {
           DB.Depart.Delete(new { DepartmentName = departnameId });
       }

       public static Address GetAddress(Guid addrId)
       {
           return DB.Addr.GetSingle(new { Id = addrId });
       }

       public static Department GetDepartment(Guid departId)
       {
           return DB.Depart.GetSingle(new { Id = departId });
       }

       public static void UpdateAddress(object values, object where)
       {
           DB.Addr.UpdateWithWhere(values: values, where: where);
       }

       public static void UpdateDepartment(object values, object where)
       {
           DB.Depart.UpdateWithWhere(values: values, where: where);
       }

       public static IEnumerable<Address> GetAddressAll()
       {
           return DB.Addr.GetAll();
       }

       public static IEnumerable<Department> GetDepartmentAll()
       {
           return DB.Depart.GetAll();
       }

    }
}
