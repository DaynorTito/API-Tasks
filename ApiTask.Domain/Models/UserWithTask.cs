namespace ApiTask.Domain.Models;

public class UserWithTasks
{
    public User User { get; set; }
    public List<global::Task> Tasks { get; set; }
}