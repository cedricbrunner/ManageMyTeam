using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ManageMyTeam.Models;

namespace ManageMyTeam.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<BaseLoad> Baseloads { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<RequirementHour> RequirementHours { get; set; }
        public DbSet<SchedulingHour> SchedulingHours { get; set; }

    }
}
