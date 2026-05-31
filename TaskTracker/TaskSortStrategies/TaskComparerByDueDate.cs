namespace TaskTracker.TaskSortStrategies
{
    public class TaskComparerByDueDate : IComparer<Task>
    {
        public int Compare(Task? x, Task? y)
        {
            bool xHasDueDate = x != null && x.DueDate != null;
            bool yHasDueDate = y != null && y.DueDate != null;

            if (xHasDueDate && yHasDueDate)
            {
                DateOnly notNullableDateX = (DateOnly)x.DueDate;
                DateOnly notNullableDateY = (DateOnly)y.DueDate;
                return notNullableDateX.CompareTo(notNullableDateY);
            }
            else if (xHasDueDate && !yHasDueDate)
                return -1;
            else if (!xHasDueDate && yHasDueDate)
                return 1;
            else
                return 0;
        }
    }
}
