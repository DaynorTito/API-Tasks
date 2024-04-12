using System;
using System.Collections.Generic;

namespace ApiTask.Api;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string Status { get; set; } = null!;
}
