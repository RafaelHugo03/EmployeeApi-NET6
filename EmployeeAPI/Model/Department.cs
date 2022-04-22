namespace EmployeeAPI.Model
{
    public class Department 
    {
        public Department(string name)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Employee> Employees { get; set; }
    }
}
