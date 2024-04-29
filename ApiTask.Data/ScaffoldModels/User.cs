namespace ApiTask.Data.ScaffoldModels;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Passwd { get; set; }

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
