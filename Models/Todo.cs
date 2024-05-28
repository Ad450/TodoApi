namespace TodoApi.Models.Todo;
using TodoApi.Models.User;


public class Todo
{

    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? SubTitle { get; set; }

    public string? TodoDescription { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid UserId { set; get; }

    public User User { set; get; }

}

