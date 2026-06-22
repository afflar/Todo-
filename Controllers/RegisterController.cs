using static BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.TodoDbContext;
using TodoList.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TodoList.Controllers;

[Route("[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly AppDbContext _db;

    public RegisterController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("reg")]
    public async Task<IActionResult> Register(RegisterUserRequest user)
    {
        if (await _db.Users.AnyAsync(n => n.Email == user.email)) return NotFound(user.email); 

        var hash = HashPassword(user.password);

        await _db.Users.AddAsync(new User
        {
            Name = user.name,
            Email = user.email,
            HashPassword = hash
        });

        await _db.SaveChangesAsync();

        return Ok("User added.");
    }

    [HttpPost("log")]
    public async Task<IActionResult> Login(
         [FromServices] JwtService jwt,
         LoginUserRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(n => n.Email == request.email);
    
        var isCorrect = Verify(request.password, user?.HashPassword ?? "$2a$11$abcdefghijklmnopqrstuuPj6mP8L2lVx0V7bQf4gJvPz7R3G");

        if (!isCorrect || user is null) return Unauthorized("Invalid email or password");

        var token = jwt.GenerateToken(user);

        return Ok(token);
    }


    [HttpGet]
    public async Task<ActionResult<List<User>>> ShowAllUsers()
    {
        var users = await _db.Users.AsNoTracking().ToListAsync();

        return users;
    }
}
