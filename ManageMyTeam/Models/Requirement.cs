/*
 * Klasse Anforderung
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
    public class RequirementHour
    {

        public int RequirementHourId { get; set; }
        public int RequirementHourAmount { get; set; }

        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime RequirementHourDate { get; set; }


        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }



    }
}
