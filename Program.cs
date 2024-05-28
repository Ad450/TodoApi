
using Microsoft.EntityFrameworkCore;
using TodoApi.Repository;
using TodoApi.Extensions.TodoApiExtension;



var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddAuthorization();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    // options.RegisterServicesFromAssemblyContaining(typeof(TodoApi));
});

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TodoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("todoConnectionString")
));
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
// .AddEntityFrameworkStores<TodoContext>(); // this allows us to retrieve the IdentityUser from DB through EF

// builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//     .AddEntityFrameworkStores<TodoContext>();

builder.Services.AddAllTodoApiServices();

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
