using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.ViewModels.Employee
{
    public class EditorEmployeeViewModel
    {
        [Required(ErrorMessage = "Insira o nome do funcionário")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Insira o cargo do funcionário")]
        public string Office { get; set; }
        [Required(ErrorMessage = "Insira o id do departamento que esse funcionário atua")]
        public int IdDepartment { get; set; }
    }
}
