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

/*
namespace ManageMyTeam.Controllers
/*    public class ReportDepartmentController : Controller
    
        private readonly ApplicationDbContext _context;

        public ReportDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportDepartmentController
        public ActionResult Index()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: ReportDepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report([Bind("DepartmentId,StartWeek,EndWeek")] ReportDepartmentModel reportDepartmentModel)
        {
            ReportResult reportResult = new ReportResult()
            {
                WeekLoadDepartment = new List<ReportResultWeekLoadDepartment>()
            };
            int startWeekIndex = GetIso8601WeekOfYear(reportDepartmentModel.StartWeek);
            int endWeekIndex = GetIso8601WeekOfYear(reportDepartmentModel.EndWeek);

            //var employee = _context.Employees.Include(e => e.Department).Include(e => e.Function).FirstOrDefaultAsync(m => m.EmployeeId == reportDepartmentModel.EmployeeId).Result;
            //var schedulingHour = _context.SchedulingHours.Include(s => s.Employee).Include(s => s.Project).FirstOrDefaultAsync(m => m.EmployeeId == reportDepartmentModel.EmployeeId).Result;
            //var baseLoad = _context.Baseloads.Include(b => b.Employee).FirstOrDefaultAsync(m => m.EmployeeId == reportDepartmentModel.EmployeeId).Result;

 

            //var absencesOfEmployeeInCurrentWeek = _context.Absences.Where(absence => absence.EmployeeId == reportDepartmentModel.EmployeeId);
            //var schedulingOfEmployeeInCurrentWeek = _context.SchedulingHours.Where(SchedulingHour => SchedulingHour.EmployeeId == reportDepartmentModel.EmployeeId);
            //var baseloadOfEmployee = _context.Baseloads.Where(BaseLoad => BaseLoad.EmployeeId == reportDepartmentModel.EmployeeId);


            for (int currentWeekIndex = startWeekIndex; currentWeekIndex <= endWeekIndex; currentWeekIndex++)
            {
                // We are in the context of a calendar week
                int scheduledHoursDepartment = 0 ;
                int demandHoursDepartment = 0;
                int MaximumHoursDepartment = 0;

                /*foreach (BaseLoad BaseLoad in baseloadOfEmployee)
                {
                }


                int countDayWithAbsences = 0;
                foreach (Absence absence in absencesOfEmployeeInCurrentWeek)
                {
                    foreach (DateTime day in EachDay(absence.AbcenceStart, absence.AbcenceEnd))
                    {
                        if (GetIso8601WeekOfYear(day) == currentWeekIndex)
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
        }*/
