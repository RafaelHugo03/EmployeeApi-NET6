namespace EmployeeAPI.Model
{
    public class TaskEmployee 
    {
        public TaskEmployee(string title, string description, Employee employee)
        {
            Title = title;
            Description = description;  
            Done = false;
            Employee = employee;
            LastUpdateDate = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public string Description { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public Employee Employee { get; private set; }

        public void UpdateTask(string title, string description, bool done, Employee employee) 
        {
            Title = title;
            Description= description;
            Done= done;
            Employee = employee;
            LastUpdateDate = DateTime.Now;   
        }

    }
}
