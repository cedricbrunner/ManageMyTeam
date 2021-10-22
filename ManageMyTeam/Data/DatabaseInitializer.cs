using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageMyTeam.Models;
using Microsoft.AspNetCore.Builder;

namespace ManageMyTeam.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                Department dep1 = new Department
                {
                    DepartmentId = 104,
                    DepartmentName = "Project Management"
                };

                // Abteilungen werden angelegt
                if (!context.Departments.Any())
                {

                    var departments = new Department[]
                    {
                        dep1,
                        new Department {  DepartmentId = 100,
                                            DepartmentName = "Execution System",},
                        new Department {  DepartmentId = 101,
                                            DepartmentName = "M2U" },
                        new Department { DepartmentId = 102,
                                            DepartmentName = "Marketing & Sales" },
                        new Department {  DepartmentId = 103,
                                            DepartmentName = "Finance & Administration" },

                };
                    foreach (Department department in departments)
                    {
                        context.Departments.Add(department);
                    }

                }
                context.SaveChanges();


                //Funktion wird erzeugt
                context.Database.EnsureCreated();
                Function func1 = new Function
                {
                    FunctionId = 1,
                    FunctionTyp = "Engineer"
                };

                //Zusätzliche Funktionen werden angelegt
                if (!context.Functions.Any())
                {
                    var functions = new Function[]
                    {
                    new Function { FunctionId = 2,
                        FunctionTyp = "Chef de cuisine" },
                    new Function { FunctionId = 3,
                        FunctionTyp = "Platform Owner" },
                    new Function { FunctionId = 4,
                        FunctionTyp = "Tester" },
                    };
                    foreach (Function function in functions)
                    {
                        context.Functions.Add(function);
                    }
                }
                context.SaveChanges();




                if (!context.Employees.Any())
                {
                    var employees = new Employee[]
                    {
                        new Employee {  EmployeeName = "Max",
                                        EmployeeId = 10,
                                        WorkID = 1000,
                                        WorkLoad = 80,
                                        Department = dep1,
                                        Function = func1
                        }

                    };
                    foreach (Employee employee in employees)
                    {
                        context.Employees.Add(employee);
                    }

                }
                context.SaveChanges();

            }
          

            catch (Exception e)
            {
                Console.WriteLine("Exception during DbInitializer: " + e.ToString());
            }

        }

        internal static void Initialize(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
    
}
