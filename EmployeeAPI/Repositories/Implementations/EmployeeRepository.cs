using EmployeeAPI.Data;
using EmployeeAPI.Model;
using EmployeeAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Employee entity)
        {
            await _context.Employess.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDelete = await _context.Employess.FirstOrDefaultAsync(x => x.Id == id);
            _context.Employess.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context
                .Employess
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context
                .Employess
                .AsNoTracking()
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Employee entity)
        {
            var toUpdate = await _context.Employess.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _context.Entry(toUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
