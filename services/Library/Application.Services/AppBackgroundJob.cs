namespace Application.Services;

public delegate void JobCaller();

public class AppBackgroundJob
{
    private Dictionary<string, JobCaller> jobs;
    private System.Timers.Timer? listen;

    public AppBackgroundJob()
    {
        this.jobs = new Dictionary<string, JobCaller>();
    }

    public void On(int interval)
    {
        this.listen = new System.Timers.Timer(interval);

        this.listen.Elapsed += (s, e) =>
        {
            this.listen.Enabled = false;
            foreach (string key in this.jobs.Keys)
                try
                { this.jobs[key].Invoke(); }
                catch (Exception ex)
                { Console.WriteLine($"JOB EXCEPTION: {key}: {ex.Message}"); }
            this.listen.Enabled = true;
        };

        this.listen.Enabled = true;
    }

    public void RegistryJob(JobCaller caller)
    {
        var id = Guid.NewGuid().ToString();
        this.jobs.Add(id, caller);
    }

    public void Off()
    {
        if (this.listen is not null)
        {
            this.listen.Enabled = false;
            this.listen.Stop();
            this.listen.Dispose();
        }
    }
}
