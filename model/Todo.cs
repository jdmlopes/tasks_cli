using System.Text.Json.Serialization;

namespace tasks_cli.model;

public class Todo
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public TodoStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        string statusText = Status switch
        {
            TodoStatus.pending => "[ ] Pending",
            TodoStatus.inProgress => "[-] In Progress",
            TodoStatus.done => "[X] Done",
            _ => throw new ArgumentException()
        };
        return
    $@"Task #{Id}
-------------------------
Description : {Description}
Status      : {statusText}
Created At  : {CreatedAt:yyyy/MM/dd HH:mm:ss}
Updated At  : {UpdatedAt:yyyy/MM/dd HH:mm:ss}";
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TodoStatus
{
    pending,
    inProgress,
    done
}

public record TodoDTO(List<Todo> Todos, int LastId);