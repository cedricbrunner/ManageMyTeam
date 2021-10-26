using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManageMyTeam.Models
{
    public class PublicHoliday
    {
        public int PublicHolidayId { get; set; }
        public string PublicHolidayTitle { get; set; }

        [BindProperty, DataType("day"), ModelBinder(BinderType = typeof(WeekOfYearAwareDateTimeModelBinder))]
        public DateTime PublicHolidayDate { get; set; }


        public int SiteId { get; set; }
        public Site Site { get; set; }



    }
}

