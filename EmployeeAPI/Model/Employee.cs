namespace EmployeeAPI.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public Department Department { get; set; }
        public List<TaskEmployee> Tasks { get; set; }
    }
}
