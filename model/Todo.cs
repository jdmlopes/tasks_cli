using System.Text.Json.Serialization;

namespace tasks_cli.model;

public class Todo
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public TodoStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TodoStatus{
    pending,
    inProgress,
    done
}

public record TodoDTO(List<Todo> Todos, int LastId);