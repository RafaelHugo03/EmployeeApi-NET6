using EmployeeAPI.Model;

namespace EmployeeAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskEmployee>> GetAll();
        Task<TaskEmployee> GetById(int id);
        Task Create(TaskEmployee task);
        Task Update(TaskEmployee task);
        Task Delete(int id);

    }
}
