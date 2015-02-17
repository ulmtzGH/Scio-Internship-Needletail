using BL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracNeedletail
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid g_Id;
            int cont = 1;
            var view = Class1.GetAddressAll();
            var view2 = Class1.GetDepartmentAll();
            Console.Write("1.- Address" + "\n" + "2.- Department" + "\n");
            Console.Write("Seleccione el nuemero de la tabla: " + "\n");
            string d = Console.ReadLine();
            switch (d)
            {
                case "1": Console.Write("Tabla Address.." + "\n");
                    Console.Write("1.- Insertar" + "\n" + "2.- Eliminar" + "\n" + "3.- Editar" + "\n" + "4.- Consultar" + "\n");
                    Console.Write("Seleccione una opción: " + "\n");
                    string t=Console.ReadLine();
                    switch (t)
                    {
                        case "1": Console.Write("Insertar..");
                            Class1.CreateAddress();
                            break;
                        case "2": Console.Write("Eliminar.. Introdusca calle del registro" + "\n");
                            //g_Id = Console.Read();
                            //Class1.DeleteAddress(g_Id);
                            
                            cont = 1;
                            foreach(Address add in view){
                                Console.WriteLine(cont + ".- " + add.Id + " " + add.Street + " " + add.ZipCode + " " + add.Phone);
                                cont = cont + 1;
                            }
                            string i_delete=Console.ReadLine();
                            
                            Class1.DeleteAddress(i_delete);
                            Console.Write("Eliminado registro calle: " + i_delete);
                            break;

                        case "3": Console.Write("Editar.. Introdusca Id Completo" + "\n");
                            cont = 1;
                            foreach (Address add in view)
                            {
                                Console.WriteLine(cont + ".- " + add.Id + " " + add.Street + " " + add.ZipCode + " " + add.Phone);
                                cont = cont + 1;
                            }
                            string a_update = Console.ReadLine();
                            Class1.UpdateAddress(a_update);
                            break;
                        case "4": Console.Write("Consultar.. " + "\n");
                            //g_Id = Console.Read();
                            
                            cont = 1;
                            foreach(Address add in view){
                                Console.WriteLine(cont + ".- " + add.Id + " " + add.Street + " " + add.ZipCode + " " + add.Phone);
                                cont = cont + 1;
                            }
                            break;
                    } 
                    Console.ReadKey();
                    break;
                case "2": Console.Write("Tabla Department.." + "\n");
                    Console.Write("1.- Insertar" + "\n" + "2.- Eliminar" + "\n" + "3.- Editar" + "\n" + "4.- Consultar" + "\n");
                    Console.Write("Seleccione una opción: " + "\n");
                    string q = Console.ReadLine();
                    switch (q)
                    {
                        case "1": Console.Write("Insertar..");
                            Class1.CreateDepartment();
                            break;
                        case "2": Console.Write("Eliminar.. Introdusca Departamento del registro" + "\n");
                            cont = 1;
                            foreach(Department dep in view2){
                                Console.WriteLine(cont + ".- " + dep.Id + " " + dep.DepartmentName + " " + dep.ParentDepartmentId);
                                cont = cont + 1;
                            }
                            string d_delete=Console.ReadLine();
                            
                            Class1.DeleteDepartment(d_delete);
                            Console.Write("Eliminado registro departamento: " + d_delete);
                            break;
                        case "3": Console.Write("Editar.. Introdusca Id Completo" + "\n");
                            cont = 1;
                            foreach (Department dep in view2)
                            {
                                Console.WriteLine(cont + ".- " + dep.Id + " " + dep.DepartmentName + " " + dep.ParentDepartmentId);
                                cont = cont + 1;
                            }
                            string d_update = Console.ReadLine();
                            Class1.UpdateDepartment(d_update);
                            
                            break;
                        case "4": Console.Write("Consultar.. " + "\n");
                            cont = 1;
                            foreach (Department dep in view2)
                            {
                                Console.WriteLine(cont + ".- " + dep.Id + " " + dep.DepartmentName + " " + dep.ParentDepartmentId);
                                cont = cont + 1;
                            }
                            break;
                    } 
                    Console.ReadKey();
                    break;
            }
            Console.ReadKey();
            
        }
    }
}
