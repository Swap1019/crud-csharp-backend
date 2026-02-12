namespace Backend.Models.DTOs;

public class CreateTodoDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime? Schedule { get; set; }
}