using TodoApi.Controllers;
using TodoApi.Models.Todo;

namespace TodoApi.Services;

public interface ITodoService
{
    // Task<List<Todo>> GetTodosAsync(GetTodosParam param);
    Task<Todo> GetTodoByIdAsync(string id);

    Task CreateTodoAsync(Todo todo);

    Task UpdateTodoAsync(Todo todo);

    Task DeleteTodoAsync(string id);

}
