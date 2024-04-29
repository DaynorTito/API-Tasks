using Task = ApiTask.Data.ScaffoldModels.Task;

namespace ApiTask.Data.ScaffoldModels;

public partial class StatusHistory
{
    public string Id { get; set; } = null!;

    public string? TaskId { get; set; }

    public string OldStatus { get; set; } = null!;

    public string NewStatus { get; set; } = null!;

    public DateOnly ChangedDate { get; set; }

    public virtual Task? Task { get; set; }
}
