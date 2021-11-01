

/*
 * Controller Auswertung Mitarbeiter
 * 10.2021 Cédric Brunner
 * 
 */
using ManageMyTeam.Data;
using ManageMyTeam.Helper;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report([Bind("EmployeeId,StartWeek,EndWeek")] ReportModel reportModel)
        {
            ReportHelper employeeReportHelper = new ReportHelper();
            ReportResult reportResult = employeeReportHelper.EmployeeReport(reportModel.StartWeek, reportModel.EndWeek, reportModel.EmployeeId, _context);

            if(reportModel == null)
            {
                return NotFound();
            }

            return View(reportResult);
        }
    }
}
