using MediatR;
using TodoApi.Controllers;
using TodoApi.Models.Todo;
using TodoApi.Services;

namespace TodoApi.Logics.Commands;
public class CreateTodo(CreateTodoRecord param) : IRequest<Unit>
{
    public CreateTodoRecord Param { get; set; } = param;
}

public class CreateTodoHandler(ITodoService todoService) : IRequestHandler<CreateTodo, Unit>
{
    public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
    {
        await todoService.CreateTodoAsync(
            new Todo
            {
                SubTitle = request.Param.subtitle,
                Title = request.Param.title,
                TodoDescription = request.Param.todoDescription,
                UserId = request.Param.userId,
            }
        );
        return Unit.Value;
    }
}