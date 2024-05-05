
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repository;

namespace TodoApi.Services;

public class TodoService(TodoContext _todoContext) : ITodoService

{

    public async Task CreateTodoAsync(Todo todo)
    {
        _todoContext.Add(todo);
        await _todoContext.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(string id)
    {
        var todo = await _todoContext.Todos.Where(t => t.Id == id).FirstOrDefaultAsync()
         ?? throw new Exception($"todo with id {id} does not exist");
        _todoContext.Remove(todo);
        await _todoContext.SaveChangesAsync();

    }

    public async Task<Todo> GetTodoByIdAsync(string id)
    {
        return await _todoContext.Todos.FindAsync(id)
        ?? throw new Exception($"todo with id {id} does not exist");
    }



    public async Task<List<Todo>> GetTodosAsync()
    {
        return await _todoContext.Todos.ToListAsync();
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        var toBeUpdateTodo = await _todoContext.Todos.FindAsync(todo.Id)
            ?? throw new Exception($"todo with id {todo.Id} does not exist");

        toBeUpdateTodo.CreatedAt = todo.CreatedAt;
        toBeUpdateTodo.Title = todo.Title;
        toBeUpdateTodo.TodoDescription = todo.TodoDescription;
        toBeUpdateTodo.SubTitle = todo.SubTitle;

        await _todoContext.SaveChangesAsync();

    }
}
