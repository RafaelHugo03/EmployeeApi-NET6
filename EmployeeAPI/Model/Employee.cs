using EmployeeAPI.Data;

namespace EmployeeAPI.Model
{
    public class Employee 
    {
        public Employee(string name, string office, Department department)
        {
            Name = name;
            Office = office;
            Department = department;
            Tasks = new List<TaskEmployee>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Office { get; private set; }
        public Department Department { get; private set; }
        public List<TaskEmployee> Tasks { get; private set; }

        public void UpdateEmployee(string name, string office, Department department) 
        {
            Name = name;
            Office = office;
            Department= department;

        }
    }
}
