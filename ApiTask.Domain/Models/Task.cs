using System;
using System.ComponentModel.DataAnnotations;

public class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string Status { get; set; }
}
