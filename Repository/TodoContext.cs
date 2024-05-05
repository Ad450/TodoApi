using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repository;

public class TodoContext(DbContextOptions<TodoContext> options) : IdentityDbContext(options)
{

    public DbSet<Todo> Todos { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Todo>().Property(x => x.Title).IsRequired();
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     //optionsBuilder.UseNpgsql(configuration.GetConnectionString("todoConnectionString"));
    // }
}