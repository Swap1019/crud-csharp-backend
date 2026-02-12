namespace Backend.Models;

public class Todo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Schedule { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Status
    {
        get
        {
            if (Schedule == null)
                return "Upcoming";

            return DateTime.UtcNow >= Schedule.Value
                ? "Done"
                : "Upcoming";
        }
    }
}
