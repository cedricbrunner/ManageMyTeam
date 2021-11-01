

/*
 * Controller Auswertung Team
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
    public class ReportDepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportController
        public ActionResult Index()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report([Bind("DepartmentId,StartWeek,EndWeek")] ReportDepartmentModel reportModel)
        {
            ReportHelper employeeReportHelper = new ReportHelper();

            ReporDepartmentResult reportResult = employeeReportHelper.DepartmentReport(reportModel.StartWeek, reportModel.EndWeek, reportModel.DepartmentId, _context);

            if (reportModel == null)
            {
                return NotFound();
            }
            return View(reportResult);
        }
    }
}
