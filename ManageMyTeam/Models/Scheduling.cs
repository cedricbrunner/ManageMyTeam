/*
 * Klasse Einplanung
 * 10.2021 Cédric Brunner
 * 
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class SchedulingHour
    {
        public int SchedulingHourId { get; set; }

        public int SchedulingHourAmount { get; set; }

        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]

        public DateTime SchedulingHourDate { get; set; }


        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
