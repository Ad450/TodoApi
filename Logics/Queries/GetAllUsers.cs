using MediatR;
using TodoApi.Controllers;
using TodoApi.Models.User;
using TodoApi.Services;

namespace TodoApi.Logics.Queries;

public class GetAllUsers(GetUserParam param) : IRequest<ICollection<User>>
{
    public GetUserParam Param { get; set; } = param;
}

public class GetAllHandler(IUserService userService) : IRequestHandler<GetAllUsers, ICollection<User>>
{
    public async Task<ICollection<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return await userService.GetUsers(request.Param);
    }
}
