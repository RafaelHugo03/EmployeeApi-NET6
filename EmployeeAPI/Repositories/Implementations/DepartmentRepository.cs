using EmployeeAPI.Data;
using EmployeeAPI.Model;
using EmployeeAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDelete = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            _context.Departments.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context
                .Departments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            return await _context
                .Departments
                .AsNoTracking()
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Department department)
        {
            var toUpdate = await _context.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);
            _context.Entry(toUpdate).CurrentValues.SetValues(toUpdate);
            await _context.SaveChangesAsync();
        }
    }
}
