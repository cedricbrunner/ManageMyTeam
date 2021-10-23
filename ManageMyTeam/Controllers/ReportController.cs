using ManageMyTeam.Data;
using ManageMyTeam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportController
        public ActionResult Index()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");

            return View();
        }
        // POST: RequirementHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Report([Bind("EmployeeId,StartWeek,EndWeek")] ReportModel reportModel)
        public IActionResult Report([Bind("EmployeeId,StartWeek,EndWeek")] ReportModel reportModel)
        {
            ReportResult reportResult = new ReportResult()
            {
                WeekLoadEmployee = new List<ReportResultWeekLoadEmployee>()
            };
            int startWeekIndex = GetIso8601WeekOfYear(reportModel.StartWeek);
            int endWeekIndex = GetIso8601WeekOfYear(reportModel.EndWeek);

            var employee =  _context.Employees.Include(e => e.Department).Include(e => e.Function).FirstOrDefaultAsync(m => m.EmployeeId == reportModel.EmployeeId).Result;
            var schedulingHour = _context.SchedulingHours.Include(s => s.Employee).Include(s => s.Project).FirstOrDefaultAsync(m => m.EmployeeId == reportModel.EmployeeId).Result;
            var baseLoad = _context.Baseloads.Include(b => b.Employee).FirstOrDefaultAsync(m => m.EmployeeId == reportModel.EmployeeId).Result;


            if (employee == null)
            {
                // error handling
                return NotFound();
            }

            var absencesOfEmployeeInCurrentWeek = _context.Absences.Where(absence => absence.EmployeeId == reportModel.EmployeeId);
            var schedulingOfEmployeeInCurrentWeek = _context.SchedulingHours.Where(SchedulingHour => SchedulingHour.EmployeeId == reportModel.EmployeeId);
            var baseloadOfEmployee = _context.Baseloads.Where(BaseLoad => BaseLoad.EmployeeId == reportModel.EmployeeId);
            

            for (int currentWeekIndex = startWeekIndex; currentWeekIndex <= endWeekIndex; currentWeekIndex++)
            {
                // We are in the context of a calendar week
                int availableHours = (employee.WorkLoad * 40) / 100;
                int targetHours = baseLoad.BaseLoadAmount;

                foreach (BaseLoad BaseLoad in baseloadOfEmployee)
                {
                }

                int WeekswithScheduling = 0;
                foreach (SchedulingHour SchedulingHour in schedulingOfEmployeeInCurrentWeek)
                {
                    //foreach (int ammount in (SchedulingHour.Sch)

                    //foreach (DateTime day in EachDay(schedulingHour.AbcenceStart, schedulingHour.AbcenceEnd))

                    WeekswithScheduling++;
                }


                int countDayWithAbsences = 0;
                foreach (Absence absence in absencesOfEmployeeInCurrentWeek)
                {
                    foreach (DateTime day in EachDay(absence.AbcenceStart, absence.AbcenceEnd))
                    {
                        if(GetIso8601WeekOfYear(day) == currentWeekIndex)
                        {
                            countDayWithAbsences++;
                        }
                    }
                }

                if (countDayWithAbsences > 5)
                {
                    countDayWithAbsences = 5;
                }

                var hoursOfAbsences = countDayWithAbsences * 8;
                availableHours = availableHours - hoursOfAbsences;

                var hoursOfScheduling = WeekswithScheduling;
                targetHours = hoursOfScheduling + targetHours;

                ReportResultWeekLoadEmployee currentReportWeek = new ReportResultWeekLoadEmployee()
                {

                    CurrentWeek = "KW " + currentWeekIndex,
                    AvailableHours = availableHours,
                    TargetHours = targetHours
                };

                reportResult.WeekLoadEmployee.Add(currentReportWeek);
            }
            return View(reportResult);
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
