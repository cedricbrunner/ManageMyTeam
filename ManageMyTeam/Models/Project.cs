/*
 * Klasse Projekt
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
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        
        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime ProjectStartDate { get; set; }

        [BindProperty, DataType("week"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime ProjectEndDate { get; set; }

   
    }
}
