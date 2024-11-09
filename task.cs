namespace Taskmanager
{
    public class Task
    {
        // Primary constructor
        public Task(int id, string description)
        {
            Id = id;
            Description = description;
        }

        // Properties with getters and setters
        public int Id { get; }
        public string Description { get; set; }  // Add a setter so we can modify the description
        public bool IsCompleted { get; set; } = false;  // Default value for IsCompleted

        // Override ToString to display the task in a user-friendly way
        public override string ToString()
        {
            return $"{Id}. {Description} - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }
}
