namespace EmployeeAPI.Model
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Office { get; set; }
        public Department Department { get; set; }
        public List<TaskEmployee> Tasks { get; set; }
    }
}
