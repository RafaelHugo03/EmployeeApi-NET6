namespace EmployeeAPI.Model
{
    public class TaskEmployee
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Employee Employee { get; set; }
    }
}
