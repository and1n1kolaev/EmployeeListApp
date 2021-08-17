using EmployeeListApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeListApp.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
