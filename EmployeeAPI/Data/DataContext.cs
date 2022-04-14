using EmployeeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employess { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<TaskEmployee> Tasks { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

    }
}
