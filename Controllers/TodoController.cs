namespace TodoApi.Controllers;

using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;
using TodoApi.Services;



[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService _todoService) : ControllerBase
{

    [HttpPost("createToken")]
    public IActionResult CreateToken()
    {
        IDictionary<string, object> claims = new Dictionary<string, object>
        {
            { ClaimTypes.Role, "Student" },
            { ClaimTypes.Role, "Admin" },

        };
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = "YourIssuerHere",
            Expires = DateTime.UtcNow.AddDays(1),
            Audience = "http://localhost:5076",
            Claims = claims,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                                    (Encoding.UTF8.GetBytes("this is my key this is my key this is my key this is my key this is my key this is my key this is my key this is my key  ")),
                                         SecurityAlgorithms.HmacSha256)

        };
        var token = new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);

        return Ok(token);
    }

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

    [Authorize(Policy = "AdminOnly")]
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


// Authentication mechanisms
// stateless JWT
// stateful JWT
// session
