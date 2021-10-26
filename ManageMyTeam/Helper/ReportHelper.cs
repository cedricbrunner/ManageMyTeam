using ManageMyTeam.Data;
using ManageMyTeam.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Helper
{
    public class ReportHelper
    {
        public ReporDepartmentResult DepartmentReport(DateTime startWeek, DateTime endweek, int departmentId, ApplicationDbContext _context)
        {
            ReporDepartmentResult reportResult = new ReporDepartmentResult()
            {
                WeekLoadDepartment = new List<ReportResultWeekLoadDepartment>()
            };

            Dictionary<string, ReportResultWeekLoadDepartment> dictionaryDepartmentWeekResult = new Dictionary<string, ReportResultWeekLoadDepartment>();

            var employees = _context.Employees.Include(e => e.Department).Include(e => e.Function).Where(m => m.DepartmentId == departmentId);

            foreach(Employee employee in employees)
            {
                ReportResult employeeResult = EmployeeReport(startWeek, endweek, employee.EmployeeId, _context);

                foreach(ReportResultWeekLoadEmployee currentWeek in employeeResult.WeekLoadEmployee)
                {
                    ReportResultWeekLoadDepartment existingValue;
                    if (!dictionaryDepartmentWeekResult.TryGetValue(currentWeek.CurrentWeek, out existingValue))
                    {

                        ReportResultWeekLoadDepartment deparmentWeek = new ReportResultWeekLoadDepartment()
                        {
                            CurrentWeekAsInt = currentWeek.CurrentWeekAsInt,
                            CurrentWeek = currentWeek.CurrentWeek,
                            DemandHoursDepartment = 0,
                            MaximumHoursDepartment = currentWeek.AvailableHours,
                            ScheduledHoursDepartment = currentWeek.TargetHours
                        };
                        dictionaryDepartmentWeekResult.Add(currentWeek.CurrentWeek, deparmentWeek);
                    }
                    else
                    {
                        existingValue.MaximumHoursDepartment += currentWeek.AvailableHours;
                        existingValue.ScheduledHoursDepartment += currentWeek.TargetHours;
                    }
                }
            }

            foreach (KeyValuePair<string, ReportResultWeekLoadDepartment> entry in dictionaryDepartmentWeekResult)
            {
                reportResult.WeekLoadDepartment.Add(entry.Value);
            }

            var requirements = _context.RequirementHours.Include(e => e.Department).Where(m => m.DepartmentId == departmentId);
            foreach(RequirementHour currentRequirementHour in requirements)
            {
                foreach(ReportResultWeekLoadDepartment currentWeek in reportResult.WeekLoadDepartment)
                {
                    if(GetIso8601WeekOfYear(currentRequirementHour.RequirementHourDate) == currentWeek.CurrentWeekAsInt)
                    {
                        currentWeek.DemandHoursDepartment += currentRequirementHour.RequirementHourAmount;
                    }
                }
            }

            return reportResult;
        }

        public ReportResult EmployeeReport(DateTime startWeek, DateTime endweek, int employeeId, ApplicationDbContext _context)
        {
            ReportResult reportResult = new ReportResult()
            {
                WeekLoadEmployee = new List<ReportResultWeekLoadEmployee>()
            };

            int startWeekIndex = GetIso8601WeekOfYear(startWeek);
            int endWeekIndex = GetIso8601WeekOfYear(endweek);

            var employee = _context.Employees.Include(e => e.Department).Include(e => e.Function).FirstOrDefaultAsync(m => m.EmployeeId == employeeId).Result;
            if (employee == null)
            {
                // error handling
                return null;
            }

            var absencesOfEmployee = _context.Absences.Where(absence => absence.EmployeeId == employeeId);
            var publicHolidays = _context.PublicHolidays; //.Where(publicHolidays => publicHolidays.SiteId == siteId);
            var schedulingOfEmployee = _context.SchedulingHours.Where(SchedulingHour => SchedulingHour.EmployeeId == employeeId);
            var baseloadOfEmployee = _context.Baseloads.Where(BaseLoad => BaseLoad.EmployeeId == employeeId);

            for (int currentWeekIndex = startWeekIndex; currentWeekIndex <= endWeekIndex; currentWeekIndex++)
            {
                // We are in the context of a calendar week
                int availableHours = (employee.WorkLoad * 40) / 100;
                int targetHours = 0;

                foreach (BaseLoad BaseLoad in baseloadOfEmployee)
                {
                    targetHours += BaseLoad.BaseLoadAmount;
                }

                int WeekswithScheduling = 0;
                foreach (SchedulingHour SchedulingHour in schedulingOfEmployee)
                {
                    if (GetIso8601WeekOfYear(SchedulingHour.SchedulingHourDate) == currentWeekIndex)
                    {
                        targetHours += SchedulingHour.SchedulingHourAmount;
                    }
                }


                int countDayWithAbsences = 0;
                foreach (Absence absence in absencesOfEmployee)
                {
                    foreach (DateTime day in EachDay(absence.AbcenceStart, absence.AbcenceEnd))
                    {
                        if (GetIso8601WeekOfYear(day) == currentWeekIndex)
                        {
                            countDayWithAbsences++;
                        }
                    }
                }

                int countDayWithpublicHolidays = 0;
                foreach (PublicHoliday publicHoliday in publicHolidays)
                {
                    if (GetIso8601WeekOfYear(publicHoliday.PublicHolidayDate) == currentWeekIndex)
                    {
                        countDayWithpublicHolidays++;
                    }
                }

                if (countDayWithAbsences > 5)
                {
                    countDayWithAbsences = 5;
                }

                if (countDayWithpublicHolidays > 5)
                {
                    countDayWithpublicHolidays = 5;
                }

                var hoursOfAbsences = (countDayWithAbsences * 8) + (countDayWithpublicHolidays * 8);
                availableHours = availableHours - hoursOfAbsences;

                var hoursOfScheduling = WeekswithScheduling;
                targetHours = hoursOfScheduling + targetHours;

                ReportResultWeekLoadEmployee currentReportWeek = new ReportResultWeekLoadEmployee()
                {
                    CurrentWeekAsInt = currentWeekIndex,
                    CurrentWeek = "KW " + currentWeekIndex,
                    AvailableHours = availableHours,
                    TargetHours = targetHours
                };

                reportResult.WeekLoadEmployee.Add(currentReportWeek);
            }

            return reportResult;
        }


        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
