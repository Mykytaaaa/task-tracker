# Code smells

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