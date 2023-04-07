using HRManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data
{
    public class HrManagementDbContext : IdentityDbContext<UserModel>
    {
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<GenderModel> Genders { get; set; }

        public HrManagementDbContext(DbContextOptions<HrManagementDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HrManagement;Trusted_Connection=True;MultipleActiveResultSets=true")
                .EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}
