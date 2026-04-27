using tasks_cli.model;

namespace tasks_cli.controller;

public class TasksController
{
    public static void AddTask(string? taskDescription)
    {
        if(taskDescription is null)
        {
            Console.WriteLine("[ERROR] No task description was provided");
            return;
        }
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            int lastId = todoDTO.LastId + 1;
            List<Todo> todos = todoDTO.Todos;
            Todo todo = new()
            {
                Id = lastId,
                Description = taskDescription,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = TodoStatus.todo
            };
            todos.Add(todo);
            JsonController.SaveTasksInJson(todos, lastId);
            Console.WriteLine($"Task added successfully (ID: {lastId})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }
}