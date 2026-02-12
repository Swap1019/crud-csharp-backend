namespace Backend.Models.DTOs;

public class EditTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime? Schedule { get; set; }
}