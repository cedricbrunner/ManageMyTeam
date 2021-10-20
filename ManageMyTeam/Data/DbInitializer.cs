/*
 DD Initializer  
 C.Brunner
 10.2021
 */

using ManageMyTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;


namespace ManageMyTeam.Data
{
    public static class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                Department dep1 = new Department {DepartmentId = 104,
                                                  DepartmentName = "Project Management" };

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
                    context.SaveChanges();
                }

                //Vorgehensmodell wird generiert (Hermes und V-Model in hinterlegt)


                /*
                //Prioritäten werden zur verfügung gestelllt
                if (!context.Priorities.Any())
                {
                    var prioritys = new Priority[]
                    {
                        new Priority { PriorityType = "Priority 1"},
                        new Priority { PriorityType = "Priority 2"},
                        new Priority { PriorityType = "Priority 3"}
        
                    };
                    foreach (Priority priority in prioritys)
                    {
                        context.Priorities.Add(priority);
                    
                    }
                    context.SaveChanges();
                }
                //Kostenarten
                if (!context.Costs.Any())
                {
                    var costs = new Cost[]
                    {
                        new Cost { CostTyp = "Software"},
                        new Cost { CostTyp = "Hardware"},
                        new Cost { CostTyp = "Service"}

                    };
                    foreach (Cost cost in costs)
                    {
                        context.Costs.Add(cost);

                    }
                    context.SaveChanges();
                }

                //Verschiedene Stati
                if (!context.Status.Any())
                {
                    var statuses = new Status[]
                    {
                        new Status { StatusType = "not launched"},
                        new Status { StatusType = "in progress"},
                        new Status { StatusType = "on hold"},
                        new Status { StatusType = "completed"},
                        new Status { StatusType = "canceled"},


                    };
                    foreach (Status status in statuses)
                    {
                        context.Status.Add(status);
                    }
                    context.SaveChanges();
                }

             */
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception during DbInitializer: " + e.ToString());
            }
           
        }
    }
}
