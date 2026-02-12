using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetMyTodos()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var todos = await _context.Todos
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Created_At)
            .ToListAsync();

        return Ok(todos);
    }

    [HttpPost("create")]
    public async Task<ActionResult<TodoDto>> CreateTodo(
        [FromBody] CreateTodoDto dto
    )
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var todo = new Todo
        {
            Title = dto.Title,
            Content = dto.Content,
            Schedule = dto.Schedule,
            Created_At = DateTime.Now,
            UserId = userId,
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return Ok(new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Content = todo.Content,
            Created_At = todo.Created_At,
            Schedule = todo.Schedule
        });
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var affected = await _context.Todos
            .Where(t => t.Id == id && t.UserId == userId)
            .ExecuteDeleteAsync();

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPut("edit")]
    public async Task<ActionResult<TodoDto>> EditTodo(
        [FromBody] EditTodoDto dto
    )
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == dto.Id && t.UserId == userId);

        if (todo == null)
            return NotFound();

        todo.Title = dto.Title;
        todo.Content = dto.Content;
        todo.Schedule = dto.Schedule;
        todo.Created_At = DateTime.Now;
        
        await _context.SaveChangesAsync();

        return Ok(new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Content = todo.Content,
            Created_At = todo.Created_At,
            Schedule = todo.Schedule,
            Status = todo.Status,
        });
    }

}