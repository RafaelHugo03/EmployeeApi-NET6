using EmployeeAPI.Model;

namespace EmployeeAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task Create(Employee entity);
        Task Update(Employee entity);
        Task Delete(int id);

    }
}
