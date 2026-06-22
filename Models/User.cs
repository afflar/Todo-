namespace TodoList.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string HashPassword { get; set; } = null!;

    public List<Note>? Notes { get; set; } = [];
}