
namespace TodoApi.Models.User;

using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Todo;

public class User
{

    public Guid Id { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }

    public List<Todo> Todos { get; set; } = [];

}

public enum UserRole
{
    Admin = 1,
    Student = 2
}

