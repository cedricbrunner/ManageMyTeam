/*
 * Klasse Grundlast
 * 10.2021 Cédric Brunner
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class BaseLoad
    {

        public int  BaseLoadId { get; set; }
        public string BaseLoadTitle { get; set; }
        public int BaseLoadAmount { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }






    }
}
