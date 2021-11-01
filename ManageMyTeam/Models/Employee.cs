/*
 * Klasse Mitarbeiter
 * 10.2021 Cédric Brunner
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int WorkID { get; set; }
        public int WorkLoad { get; set; }

        public int FunctionId { get; set; }
        public Function Function { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }







    }
}
