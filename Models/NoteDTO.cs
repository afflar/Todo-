namespace TodoList.Models;

public record NoteDTO(Guid id, string title, string content, DateTime createDate);