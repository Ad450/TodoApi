using TodoApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using TodoApi.Services.UserService;
using TodoApi.Models.User;

namespace TodoApi.Extensions.TodoApiExtension;
static public class TodoApiExtension
{
    public static IServiceCollection AddAllTodoApiServices(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                    {
                        // options.Audience = "localhost";
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my key this is my key this is my key this is my key this is my key this is my key this is my key this is my key  ")),
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "YourIssuerHere",

                        };

                    }
                );

        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy =>
               {
                   policy.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString());
               });

        services.AddControllers();

        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}