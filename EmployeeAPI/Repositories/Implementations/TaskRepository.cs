using EmployeeAPI.Data;
using EmployeeAPI.Model;
using EmployeeAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context   )
        {
            _context = context;
        }

        public async Task Create(TaskEmployee task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync(); 
        }

        public async Task Delete(int id)
        {
            var toDelete = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            _context.Tasks.Remove(toDelete);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<TaskEmployee>> GetAll()
        {
            return await _context.Tasks.AsNoTracking().ToListAsync();
        }

        public async Task<TaskEmployee> GetById(int id)
        {
            return await _context
                .Tasks
                .AsNoTracking()
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(TaskEmployee task)
        {
            var toUpdate = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == task.Id);
            _context.Entry(toUpdate).CurrentValues.SetValues(task);
            await _context.SaveChangesAsync();
        }
    }
}
