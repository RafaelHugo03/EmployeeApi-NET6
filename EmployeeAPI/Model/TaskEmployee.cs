namespace EmployeeAPI.Model
{
    public class TaskEmployee : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Employee Employee { get; set; }
    }
}
