using EmployeeAPI.Model;

namespace EmployeeAPI.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int id);
        Task Create(Department department);
        Task Update(Department department);
        Task Delete(int id);

    }
}
