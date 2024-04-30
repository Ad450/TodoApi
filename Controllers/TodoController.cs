namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repository;
using TodoApi.Services;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    private readonly ITodoService _todoService = todoService;

    // public ITodoService _todoService => HttpContext.RequestServices.GetService<TodoService>();


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _todoService.GetTodoByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _todoService.GetTodosAsync());
        // return Ok(await _todoContext.Todos.ToListAsync());
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] Todo todo)
    {
        await _todoService.CreateTodoAsync(todo);
        return Created();
    }


    [HttpPut()]
    public async Task<IActionResult> Update([FromBody] Todo todo)
    {
        await _todoService.UpdateTodoAsync(todo);
        return NoContent();
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        await _todoService.DeleteTodoAsync(id);
        return NoContent();
    }



}

