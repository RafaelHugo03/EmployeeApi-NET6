using EmployeeAPI.Data;
using EmployeeAPI.Extensions;
using EmployeeAPI.Model;
using EmployeeAPI.ViewModels;
using EmployeeAPI.ViewModels.TaskEmployee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    public class TaskEmployeeController : ControllerBase
    {
        [HttpGet("api/v1/tasks")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context) 
        {
            try
            {
                var count = await context.Tasks.AsNoTracking().CountAsync();
                var tasks = await context
                    .Tasks
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Select(x => new ListTaskEmployeeViewModel 
                    {
                        Title = x.Title,
                        Description = x.Description,
                        Done = x.Done,
                        LastUpdateDate = x.LastUpdateDate
                    }).ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    Total = count,
                    tasks
                }));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05X12 - Falha interna no servidor"));
            }
        }
        [HttpGet("api/v1/tasks/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] DataContext context, [FromRoute] int id)
        {
            try
            {
                var task = await context.Tasks.AsNoTracking().Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    return NotFound(new ResultViewModel<TaskEmployee>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<TaskEmployee>(task));
                

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("05X12 - Falha interna no servidor"));
            }
        }
        [HttpPost("api/v1/tasks")]
        public async Task<IActionResult> PostAsync([FromServices] DataContext context, [FromBody] CreateTaskEmployeeViewModel model) 
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<TaskEmployee>(ModelState.GetErrors()));
            try
            {
                var employee = await context.Employess.FirstOrDefaultAsync(x => x.Id == model.IdEmployee);
                var task = new TaskEmployee(model.Title, model.Description, employee);
                
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<TaskEmployee>(task));
            }
            catch (Exception ex)
            {
               return StatusCode(500, new ResultViewModel<string>("05X12 - Falha interna no servidor"));
            }
        }
        [HttpPut("api/v1/tasks/{id:int}")]
        public async Task<IActionResult> PutAsync([FromServices] DataContext context, [FromBody] EditTaskEmployeeViewModel model, [FromRoute] int id) 
        {
            try
            {
                var taskToUpdate = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                if (taskToUpdate == null)
                    return NotFound(new ResultViewModel<TaskEmployee>("Conteúdo não encontrado"));

                var employee = await context.Employess.FirstOrDefaultAsync(x => x.Id == model.IdEmployee);

                taskToUpdate.UpdateTask(model.Title, model.Description, model.Done, employee);
                
                context.Tasks.Update(taskToUpdate);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<TaskEmployee>(taskToUpdate));
            }
            catch(Exception)
            {
                return StatusCode(500, new ResultViewModel<TaskEmployee>("05X12 - Falha interna no servidor"));
            }
        }
        [HttpDelete("api/v1/tasks/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices] DataContext context, [FromRoute] int id) 
        {
            try
            {
                var taskToDelete = await context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

                if (taskToDelete == null)
                    return NotFound(new ResultViewModel<TaskEmployee>("Conteúdo não encontrado"));

                context.Tasks.Remove(taskToDelete);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<string>("Tarefa excluída com sucesso", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<TaskEmployee>("05X12 - Falha interna no servidor"));
            }
        }
    }
}
