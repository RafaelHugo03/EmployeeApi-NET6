using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.ViewModels.Department
{
    public class EditorDepartmentViewModel
    {
        [Required(ErrorMessage = "Informe o nome do departamento")]
        public string Name { get; set; }
    }
}
