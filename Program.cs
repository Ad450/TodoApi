using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApi.Repository;
using TodoApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;



var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddAuthorization();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TodoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("todoConnectionString")
));
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
// .AddEntityFrameworkStores<TodoContext>(); // this allows us to retrieve the IdentityUser from DB through EF

// builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//     .AddEntityFrameworkStores<TodoContext>();

builder.Services.AddAuthentication(o =>
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

});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy =>
       {
           policy.RequireClaim(ClaimTypes.Role, "Admin");
       });

builder.Services.AddControllers();

builder.Services.AddScoped<ITodoService, TodoService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// 