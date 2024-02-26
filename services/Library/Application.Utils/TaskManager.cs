namespace Application.Utils;

public class TaskManager
{
    private List<Task> tasks = new List<Task>();

    private TaskManager() { }

    public static TaskManager GetInstance()
        => new TaskManager();

    public TaskManager Add(Task task)
    {
        this.tasks.Add(task);
        return this;
    }

    public TaskManager Add(List<Task> _tasks)
    {
        this.tasks.AddRange(_tasks);
        return this;
    }

    public Task AwaitAll()
    {
        Task.WaitAll(this.tasks.ToArray());
        return Task.CompletedTask;
    }
}
