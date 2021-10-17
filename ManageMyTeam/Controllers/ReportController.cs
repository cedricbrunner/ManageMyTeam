using ManageMyTeam.Data;
using ManageMyTeam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            /*
             * Create Report Model
             * 
             * neue Klasse
             *      
             *      x-achse: Kalenderwochen
             *      
             *      pro Kalenderwoche 
             *     
             *      List<KalenderwocheReportEmployye>
             *      
             *    KalenderwocheReportEmployye
             *       int auslaustung
             *       
             * 
             * */
            /*var baseLoads = _context.Baseloads.Select(e => e.EmployeeId == reportModel.EmployeeId);
            var publicHolidays = _context.PublicHolidays;
            var absences = _context.Absences.Select(e => ;
            var employee = _context.*/


            // n - wochen
            ReportResult reportResult = new ReportResult()
            {
                WeekLoadEmployee = new List<ReportResultWeekLoadEmployee>()
            };
            var totalWeeks  = (reportModel.EndWeek - reportModel.StartWeek).Days > 0 ? (reportModel.EndWeek - reportModel.StartWeek).Days / 7 : 1;
            //var currentWeek = GetIso8601WeekOfYear(reportModel.StartWeek);
            var currentWeek = 10;
            //for (int qi = 0; qi < totalWeeks; qi++)
            for (int qi = 0; qi < 10; qi++)
            {
                ReportResultWeekLoadEmployee currentReportWeek = new ReportResultWeekLoadEmployee()
                {
                    CurrentWeek = currentWeek.ToString(),
                    Workload = 80
                };

                reportResult.WeekLoadEmployee.Add(currentReportWeek);
            }
            return View(reportResult);
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
