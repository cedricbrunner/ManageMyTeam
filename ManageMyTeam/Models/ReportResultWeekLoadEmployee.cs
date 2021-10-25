using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class ReportResultWeekLoadEmployee
    {
        public  int CurrentWeekAsInt { get; set; }
        public string CurrentWeek { get; set; }
        public int TargetHours { get; set; }
        public int AvailableHours { get; set; }
    }
}
