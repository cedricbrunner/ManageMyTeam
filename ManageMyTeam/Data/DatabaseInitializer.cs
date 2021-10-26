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
                Site site1 = new Site
                {
                    SiteLocation = "Bern"
                };

                // Abteilungen werden angelegt
                if (!context.Sites.Any())
                {
                    var sites = new Site[]
                    {
                        site1,
                        new Site { SiteLocation = "Broadmeadows"},
                        new Site { SiteLocation = "Kankakee"},
                        new Site { SiteLocation = "Marburg"},
                        new Site { SiteLocation = "Wuhan"},
                        new Site { SiteLocation = "Liverpool"},
                        new Site { SiteLocation = "King of Prussia"},                      
                };
                    foreach (Site site in sites)
                    {
                        context.Sites.Add(site);
                    }

                }
                context.SaveChanges();

                context.Database.EnsureCreated();
                Department dep1 = new Department
                {
                   
                    DepartmentName = "Execution System Utilities and Building Management", Site = site1
                };

                // Abteilungen werden angelegt
                if (!context.Departments.Any())
                {

                    var departments = new Department[]
                    {
                        dep1,
                        new Department { DepartmentName = "Execution System Bulk", Site = site1},
                        new Department { DepartmentName =  "Execution System Fill Finish", Site = site1},
                        new Department { DepartmentName = "Execution System Base Fractionation", Site = site1 },
                        new Department { DepartmentName = "Execution System Packaging", Site = site1 },

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
                    
                    FunctionTyp = "Engineer"
                };

                //Zusätzliche Funktionen werden angelegt
                if (!context.Functions.Any())
                {
                    var functions = new Function[]
                    {
                    new Function { 
                        FunctionTyp = "Platform Owner" },
                    new Function { 
                        FunctionTyp = "Manager" },
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
                        new Employee {  EmployeeName = "Cédric Brunner",
                                        
                                        WorkID = 35960,
                                        WorkLoad = 90,
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

        //internal static void Initialize(IApplicationBuilder app)
        //{
          //  throw new NotImplementedException();
        //}
    }
    
}
