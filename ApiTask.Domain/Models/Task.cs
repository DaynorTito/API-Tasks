using System;
using System.ComponentModel.DataAnnotations;

public class Task
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The title is required.")]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Date of creation is required.")]
    public DateTime CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    [Required(ErrorMessage = "The state is required.")]
    public string Status { get; set; }
}
