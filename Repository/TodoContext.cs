using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Todo;
using TodoApi.Models.User;
using TodoApi.Repository.ModelConfigs;

namespace TodoApi.Repository;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{

    public DbSet<Todo> Todos { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserModelBuilderConfiguration());
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     //optionsBuilder.UseNpgsql(configuration.GetConnectionString("todoConnectionString"));
    // }
}