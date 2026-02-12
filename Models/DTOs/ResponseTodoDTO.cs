namespace Backend.Models.DTOs;
public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Created_At { get; set; }
    public DateTime? Schedule { get; set; }
    public string Status { get; set; } = null!;
}