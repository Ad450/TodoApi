using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<List<Todo>> GetTodosAsync();
    Task<Todo> GetTodoByIdAsync(string id);

    Task CreateTodoAsync(Todo todo);

    Task UpdateTodoAsync(Todo todo);

    Task DeleteTodoAsync(string id);

}
