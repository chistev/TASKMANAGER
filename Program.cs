class Program
{
    // Make tasks a read-only field. It can only be assigned once.
    static readonly List<Taskmanager.Task> tasks = new List<Taskmanager.Task>();

    static readonly string filePath = "tasks.txt";  // File to store tasks

    static void Main()
    {
        LoadTasks();  // Load tasks from file
        ShowMenu();   // Show the main menu to interact with the user
    }

    static void ShowMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Task Manager");
        Console.WriteLine("1. View Tasks");
        Console.WriteLine("2. Add Task");
        Console.WriteLine("3. Update Task");
        Console.WriteLine("4. Delete Task");
        Console.WriteLine("5. Mark Task as Completed/Incomplete");
        Console.WriteLine("6. Save and Exit");
        Console.Write("Choose an option: ");
        
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ViewTasks();
                break;
            case "2":
                AddTask();
                break;
            case "3":
                UpdateTask();
                break;
            case "4":
                DeleteTask();
                break;
            case "5":
                ToggleTaskCompletion();
                break;
            case "6":
                SaveTasks();
                return;  // Exit the program
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}

    static void ViewTasks()
{
    Console.Clear();
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks available.");
    }
    else
    {
        Console.WriteLine("Current Tasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }
    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}

   static void AddTask()
{
    Console.Clear();
    string? description = null;

    // Loop until a valid description is entered
    while (string.IsNullOrEmpty(description))
    {
        Console.Write("Enter task description: ");
        description = Console.ReadLine();  // Nullable string to handle possible null values

        // Check if description is null or empty
        if (string.IsNullOrEmpty(description))
        {
            Console.WriteLine("Description cannot be empty. Please try again.");
        }
    }
    
    // After a valid description is entered, generate a new task ID
    int id = tasks.Count + 1;  // Generate a new task ID
    Taskmanager.Task task = new(id, description);
    tasks.Add(task);  // Add the new task to the list
    Console.WriteLine("Task added!");
    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}

    static void UpdateTask()
{
    Console.Clear();
    int id = 0;

    // Loop until a valid task ID is entered
    while (id <= 0)
    {
        Console.Write("Enter task ID to update: ");
        string? input = Console.ReadLine();  // Nullable string from Console.ReadLine()

        // Try parsing the input to an integer
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out id) || id <= 0)
        {
            Console.WriteLine("Invalid ID. Please enter a valid positive number.");
        }
    }

    var task = tasks.Find(t => t.Id == id);  // Find the task by ID
    if (task != null)
    {
        Console.Write("Enter new description: ");
        string? newDescription = Console.ReadLine();  // Nullable string from Console.ReadLine()

        if (string.IsNullOrEmpty(newDescription))
        {
            Console.WriteLine("Description cannot be empty.");
        }
        else
        {
            task.Description = newDescription;  // Update task description
            Console.WriteLine("Task updated!");
        }
    }
    else
    {
        Console.WriteLine("Task not found.");
    }

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}

static void DeleteTask()
{
    Console.Clear();
    int id = 0;

    // Loop until a valid task ID is entered
    while (id <= 0)
    {
        Console.Write("Enter task ID to delete: ");
        string? input = Console.ReadLine();  // Nullable string from Console.ReadLine()

        // Try parsing the input to an integer
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out id) || id <= 0)
        {
            Console.WriteLine("Invalid ID. Please enter a valid positive number.");
        }
    }

    var task = tasks.Find(t => t.Id == id);  // Find the task by ID
    if (task != null)
    {
        tasks.Remove(task);  // Remove the task from the list
        Console.WriteLine("Task deleted!");
    }
    else
    {
        Console.WriteLine("Task not found.");
    }

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}
    // Load tasks from the text file

static void ToggleTaskCompletion()
{
    Console.Clear();
    int id = 0;

    // Loop until a valid task ID is entered
    while (id <= 0)
    {
        Console.Write("Enter task ID to toggle completion: ");
        string? input = Console.ReadLine();  // Nullable string from Console.ReadLine()

        // Try parsing the input to an integer
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out id) || id <= 0)
        {
            Console.WriteLine("Invalid ID. Please enter a valid positive number.");
        }
    }

    var task = tasks.Find(t => t.Id == id);  // Find the task by ID
    if (task != null)
    {
        // Toggle the completion status
        task.IsCompleted = !task.IsCompleted;

        // Display the new status
        string status = task.IsCompleted ? "Completed" : "Pending";
        Console.WriteLine($"Task {task.Id} is now marked as {status}.");
    }
    else
    {
        Console.WriteLine("Task not found.");
    }

    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    int id = int.Parse(parts[0]);
                    string description = parts[1];
                    bool isCompleted = bool.Parse(parts[2]);
                    Taskmanager.Task task = new(id, description) { IsCompleted = isCompleted };
                    tasks.Add(task);
                }
            }
        }
    }

    // Save tasks to the text file
    static void SaveTasks()
    {
        var lines = new List<string>();
        foreach (var task in tasks)
        {
            lines.Add($"{task.Id}|{task.Description}|{task.IsCompleted}");
        }
        File.WriteAllLines(filePath, lines);  // Write all tasks to the file
        Console.WriteLine("Tasks saved!");
    }
}
