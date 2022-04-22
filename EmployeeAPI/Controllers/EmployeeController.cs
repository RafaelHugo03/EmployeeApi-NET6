using EmployeeAPI.Data;
using EmployeeAPI.Extensions;
using EmployeeAPI.Model;
using EmployeeAPI.ViewModels;
using EmployeeAPI.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {

        [HttpGet("api/v1/employees")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            try
            {
                var count = await context.Employess.AsNoTracking().CountAsync();
                var employees = await
                    context
                    .Employess
                    .AsNoTracking()
                    .Include(x => x.Tasks)
                    .Select(x => new EditorEmployeeViewModel 
                    {
                        Name = x.Name,
                        Office = x.Office,
                        IdDepartment = x.Department.Id
                    })
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    Total = count,
                    employees
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05x11 - Falha interna no servidor"));
            }
        }
        [HttpGet("api/v1/employees/{id:int}")]
        public async Task<IActionResult> GetByid([FromServices] DataContext context, [FromRoute] int id)
        {
            try
            {
                var employee = await 
                    context
                    .Employess
                    .AsNoTracking()
                    .Include(x => x.Tasks)
                    .Include(x => x.Department)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                    return NotFound(new ResultViewModel<Employee>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Employee>(employee));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05x11 - Falha interna no servidor"));
            }
        }
        [HttpPost("api/v1/employees")]
        public async Task<IActionResult> Post([FromServices] DataContext context, [FromBody] EditorEmployeeViewModel model) 
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<Employee>>(ModelState.GetErrors()));
            try
            {
                var department = await 
                    context
                    .Departments
                    .FirstOrDefaultAsync(x => x.Id == model.IdDepartment);

                var employee = new Employee(model.Name, model.Office, department);
                

                await context.Employess.AddAsync(employee);
                await context.SaveChangesAsync();

                return Created($"api/v1/employees/{employee.Id}", new ResultViewModel<Employee>(employee));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05x11 - Falha interna no servidor"));
            }
        }
        [HttpPut("api/v1/employees/{id:int}")]
        public async Task<IActionResult> PutAsync([FromServices] DataContext context,
            [FromBody] EditorEmployeeViewModel model, [FromRoute] int id)
        {
            try
            {
                var employeeToUpdate = await 
                    context
                    .Employess
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (employeeToUpdate == null)
                    return NotFound(new ResultViewModel<Employee>("Conteúdo não encontrado"));

                var department = await context.Departments.FirstOrDefaultAsync(x => x.Id == model.IdDepartment);

                employeeToUpdate.UpdateEmployee(model.Name, model.Office, department);  

                context.Employess.Update(employeeToUpdate);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Employee>(employeeToUpdate));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05X11 - Falha interna no servidor"));
            }
        }
        [HttpDelete("api/v1/employees/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] DataContext context) 
        {
            try
            {
                var employeeToDelete = await 
                    context
                    .Employess
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (employeeToDelete == null)
                    return NotFound(new ResultViewModel<Employee>("Conteúdo não encontrado"));

                context.Employess.Remove(employeeToDelete);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<string>("Funcionário excluído com sucesso", null));
            }
            catch (Exception) 
            {
                return StatusCode(500, new ResultViewModel<string>("05X11 - Falha interna no servidor"));
            }

        }
    }
}
