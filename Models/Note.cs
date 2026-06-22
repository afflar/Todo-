namespace TodoList.Models;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Без названия";
    public string Content { get; set; } = "";
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public User? User { get; set; } = null;
}