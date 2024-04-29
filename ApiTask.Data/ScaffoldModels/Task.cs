namespace ApiTask.Data.ScaffoldModels;

public partial class Task
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Priority { get; set; }

    public string? GroupId { get; set; }

    public string? UserId { get; set; }

    public virtual GroupTask? Group { get; set; }

    public virtual ICollection<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();

    public virtual User? User { get; set; }
}
