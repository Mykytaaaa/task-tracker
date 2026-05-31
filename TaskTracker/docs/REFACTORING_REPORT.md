# Code smells

## See 'Refactoring session' section of the Final Report for metrics

## 1: Magic values in Task:
Priority and DueDate default values (-1 and DateOnly.MaxValue respectively) create mess and necessity to check functions HasPriority() and HasDueDate().

public int Priority { get; set; }
public DateOnly DueDate { get; set; }
private const int defaultPriority = -1;
private readonly DateOnly defaultDueDate = DateOnly.MaxValue;

### Solution:
Switch to nullable types:

public int? Priority { get; set; }
public DateOnly? DueDate { get; set; }

## 2. Duplicate code in 4 UserSession methods:

if (tasks == null || tasks.Count == 0)
    ui.WriteLine("No tasks found.");
else
{
    foreach (var task in tasks)
        ui.WriteLine(task.ToText());
}

### Solution: extract the code to method ListTasks and call by ListTasks(tasks):

private void ListTasks(List<Task>? tasks)
{
    if (tasks == null || tasks.Count == 0)
        ui.WriteLine("No tasks found.");
    else
    {
        foreach (var task in tasks)
            ui.WriteLine(task.ToText());
    }
}

## 3. Duplicate code:
"path + "tasks.json"" appears 3 times in JsonFileSaver

### Solution - introduce constant for file name:

private const string filename = "tasks.json";