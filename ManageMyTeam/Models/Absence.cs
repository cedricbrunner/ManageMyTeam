/*
 * Klasse Absenz
 * 10.2021 Cédric Brunner
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class Absence
    {
        public int AbsenceId { get; set; }
        public string AbcenceName { get; set; }
        public DateTime AbcenceStart { get; set; }
        public DateTime AbcenceEnd { get; set; }


        public int EmployeeId { get; set; }
        public Employee EmployeeName { get; set; }







    }
}
