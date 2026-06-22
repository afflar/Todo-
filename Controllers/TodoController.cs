using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Models;
using TodoList.TodoDbContext;


namespace TodoList.Controllers;

[Route("[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _db;

    public TodoController(AppDbContext db)
    {
        _db = db;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteRequest request)
    {
        var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await _db.Notes.AddAsync(new Note
        {
            Title = request.title,
            Content = request.content,
            UserId = id
        });

        await _db.SaveChangesAsync();

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid Id)
    {
        int isDeleted = await _db.Notes.Where(n => n.Id == Id).ExecuteDeleteAsync();

        if (isDeleted == 0) return NotFound();

        return Ok();
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Edit(Guid id, string? title, string? content)
    {
        var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);

        if (note == null) 
            return NotFound();

        if (title != null) 
            note.Title = title;

        if (content != null) 
            note.Content = content;

        await _db.SaveChangesAsync();

        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ShowAll()
    {
        var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var notes = await _db.Notes
            .Where(n => n.UserId == id)
            .Select(n => new NoteDTO(n.Id, n.Title, n.Content, n.CreateDate))
            .ToListAsync();

        return Ok(notes);
    }
}