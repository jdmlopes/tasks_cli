using System.Text.Json;
using tasks_cli.model;

namespace tasks_cli.controller;

public class JsonController
{
    public static TodoDTO GetTasksFromJson()
    {
        if(!File.Exists("tasks.json"))
            return new TodoDTO(new List<Todo>(), 0);
        
        string json = File.ReadAllText("tasks.json");
        TodoDTO? todoDTO = JsonSerializer.Deserialize<TodoDTO>(json);
        return todoDTO!;
    }
    public static void SaveTasksInJson(List<Todo> tasks, int LastId)
    {
        TodoDTO todoDTO = new TodoDTO(tasks, LastId);
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllTextAsync("tasks.json", JsonSerializer.Serialize(todoDTO, options));
    }
}