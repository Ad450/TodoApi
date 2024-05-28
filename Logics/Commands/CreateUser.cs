
using MediatR;
using TodoApi.Controllers;
using TodoApi.Services;

namespace TodoApi.Logics.Commands;

public class CreateUser(UserRecord param) : IRequest<Unit>
{
    public UserRecord Param { get; set; } = param;
}

public class CreateUserHandler(IUserService userService) : IRequestHandler<CreateUser, Unit>
{
    public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        await userService.CreateUser(request.Param);
        return Unit.Value;
    }
}