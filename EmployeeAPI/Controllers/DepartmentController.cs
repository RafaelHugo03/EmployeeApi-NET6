using EmployeeAPI.Data;
using EmployeeAPI.Model;
using EmployeeAPI.ViewModels;
using EmployeeAPI.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    public class DepartmentController : Controller
    {
        
        [HttpGet("api/v1/departments")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            try
            {
                var count = await context.Departments.AsNoTracking().CountAsync();
                var departments = await 
                    context
                    .Departments.AsNoTracking()
                    .Include(x => x.Employees)
                    .Select(x => new ListDepartmentViewModel
                    {
                        Name = x.Name
                    })
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new 
                {
                    Total = count,
                    departments
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05X10 - Falha interna no servidor"));
            }
        }
        [HttpGet("api/v1/departments/{id:int}")]
        public async Task<IActionResult> GetById([FromServices] DataContext context,[FromRoute] int id) 
        {
            try
            {
                var employee = await context
                    .Departments
                    .AsNoTracking()
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (employee == null)
                    return NotFound(new ResultViewModel<Department>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Department>(employee));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05X10 - Falha interna no servidor"));
            }
        }
    }
}
