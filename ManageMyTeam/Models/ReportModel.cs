/*
 * Klasse Auswertungs Model Mitarbeiter
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
    public class ReportModel
    {
        public int EmployeeId { get; set; }

        public int PublicHolidayId { get; set; }

        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime StartWeek { get; set; }

        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime EndWeek { get; set; }
    }
}
