using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class ReportResultWeekLoadDepartment
    {
        public int CurrentWeekAsInt { get; set; }

        public string CurrentWeek { get; set; }
        public int ScheduledHoursDepartment { get; set; }
        public int DemandHoursDepartment { get; set; }
        public int MaximumHoursDepartment { get; set; }
    }
}
