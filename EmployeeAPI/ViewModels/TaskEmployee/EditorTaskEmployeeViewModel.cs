using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.ViewModels.TaskEmployee
{
    public class EditorTaskEmployeeViewModel
    {
        [Required(ErrorMessage = "Insira um título para essa tarefa")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Insira uma descrição para essa tarefa")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Insira o id do funcionário que irá realizar a tarefa!")]
        public int IdEmployee { get; set; }
    }
}
