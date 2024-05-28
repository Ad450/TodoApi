
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Controllers;
using TodoApi.Models.User;
using TodoApi.Repository;

namespace TodoApi.Services.UserService;

public class UserService(TodoContext _context) : IUserService

{

    public async Task<object> CreateUser(UserRecord user)
    {
        try
        {
            var _user = new User
            {
                Role = user.Role,
                Username = user.Username,
                Email = user.Email,
            };
            await _context.Users.AddAsync(_user);

            var result = await _context.SaveChangesAsync();

            if (result == 0) throw new Exception("error saving");
            var token = CreateToken(_user.Role, _user.Id.ToString());
            return new
            {
                token,
                // userId = _user.Id.ToString(),
            };
        }
        catch (Exception e)
        {
            throw new Exception($"Error creating user, {e}");
        }

    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _context.Users.FindAsync(id) ?? throw new Exception("user not found");
    }

    public async Task<List<User>> GetUsers(GetUserParam param)
    {
        IQueryable<User> users = _context.Users;
        // users = users.Where(u => u.Username.Contains(param.SearchTerm) || u.Email.Contains(param.SearchTerm));
        if (param.SearchTerm != null || param.sortProperty != null)
        {
            users = users.Where(BuildSearchExpression<User>(param.SearchTerm, ["Username", "Email"]));
        }
        // users = users.Where(u => FilterProperties<User>(u, param.SearchTerm));
        return await users.ToListAsync();
    }

    public bool FilterProperties<T>(T entity, string? searchTerm) where T : class
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            // if (!(property.Name == "Todos" || property.Name == "Role")) // Using || for OR condition
            // {
            // }
            var value = property.GetValue(entity);
            if (value != null && value.GetType() == typeof(string))
            {
                if (value.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }
        }
        return false;
    }


    // public static Expression<Func<T, bool>> BuildSearchExpression<T>(string searchTerm, params string[] propertyNames)
    // {
    //     var parameter = Expression.Parameter(typeof(T), "x");
    //     var body = propertyNames
    //         .Select(propertyName => Expression.Equal(
    //             Expression.PropertyOrField(parameter, propertyName),
    //             Expression.Constant(searchTerm)))
    //         .Aggregate((a, b) => Expression.Or(a, b));

    //     return Expression.Lambda<Func<T, bool>>(body, parameter);
    // }

    public static Expression<Func<T, bool>> BuildSearchExpression<T>(string searchTerm, params string[] propertyNames)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        var propertyChecks = propertyNames
            .Select(propertyName =>
                Expression.Call(
                    Expression.PropertyOrField(parameter, propertyName),
                    containsMethod,
                    Expression.Constant(searchTerm)));

        var body = propertyChecks.Aggregate<Expression, Expression>(null, (current, propertyCheck) =>
            current == null ? propertyCheck : Expression.OrElse(current, propertyCheck));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }


    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    private string CreateToken(UserRole role, string userId)
    {
        IDictionary<string, object> claims = new Dictionary<string, object>
        {
            { ClaimTypes.Role, role.ToString()},
            { ClaimTypes.Sid, userId},

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
        return token;
    }
}



