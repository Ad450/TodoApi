namespace TodoApi.Controllers;

using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Logics.Commands;
using TodoApi.Logics.Queries;
using TodoApi.Models.Todo;
using TodoApi.Models.User;
using TodoApi.Services;



[ApiController]
[Route("api/[controller]")]
public class TodoController(IMediator mediator, IUserService userService) : ControllerBase
{

    [HttpPost("user")]
    public async Task<IActionResult> CreateUser([FromBody] UserRecord record)
    {
        return Ok(await mediator.Send(new CreateUser(record)));
    }

    // [HttpPost("login")]
    // [Authorize(Policy = "Admin")]
    // public async Task<IActionResult> Login(LoginRecord record)
    // {
    //     try
    //     {
    //         var userId = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault();
    //         if (userId.Value.ToString() != record.Id.ToString()) return StatusCode(403);

    //         var user = await _userService.GetUserById(record.Id);
    //         return Ok(user);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e);
    //     }
    //     // we checked password

    // }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(string id)
    // {

    //     return Ok(await _todoService.GetTodoByIdAsync(id));
    // }


    // // [HttpGet]
    // // public async Task<IActionResult> GetAll([FromQuery] GetTodosParam param)
    // // {
    // //     return Ok(await _todoService.GetTodosAsync(param));
    // //     // return Ok(await _todoContext.Todos.ToListAsync());
    // // }

    [HttpGet("user")]
    public async Task<IActionResult> GetUsers([FromQuery] GetUserParam param)
    {
        return Ok(await mediator.Send(new GetAllUsers(param: param)));
        // return Ok(await _todoContext.Todos.ToListAsync());
    }

    // [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRecord todo)
    {
        await mediator.Send(new CreateTodo(todo));
        return Created();
    }


    // [HttpPut()]
    // public async Task<IActionResult> Update([FromBody] Todo todo)
    // {
    //     await _todoService.UpdateTodoAsync(todo);
    //     return NoContent();
    // }

    // [HttpDelete()]
    // public async Task<IActionResult> Delete([FromQuery] string id)
    // {
    //     await _todoService.DeleteTodoAsync(id);
    //     return NoContent();
    // }

    // [HttpPost("pagination")]
    // public IActionResult Get(PaginationRecord record)
    // {
    //     List<Todo> todos = Enumerable.Range(0, 100).Select(i => new Todo()
    //     {
    //         Id = i.ToString(),
    //         Title = "We have same title",
    //         SubTitle = "We have same subtitle",
    //         CreatedAt = DateTime.Now
    //     }).ToList();

    //     if (record.Page > (int)Math.Round((decimal)todos.Count / record.PageSize))
    //     {
    //         return BadRequest();
    //     }

    //     int skip = record.Page == 1 ? record.PageSize : (record.Page - 1) * record.PageSize;

    //     return Ok(todos.Skip(skip).Take(record.PageSize));
    //     // return Ok(todos);

    // }



}


// Authentication mechanisms
// stateless JWT
// stateful JWT
// session


public class UserRecord
{
    public string Username { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }
}

public record LoginRecord(Guid Id);

public record GetUserParam(string? SearchTerm, string? sortProperty);

public record CreateTodoRecord(string title, string subtitle, string todoDescription, Guid userId);