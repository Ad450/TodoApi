
using TodoApi.Controllers;
using TodoApi.Models.User;

namespace TodoApi.Services;

public interface IUserService
{
    public Task<object> CreateUser(UserRecord user);

    public Task UpdateUser(User user);

    public Task<User> GetUserById(Guid id);

    public Task<List<User>> GetUsers(GetUserParam param);
}