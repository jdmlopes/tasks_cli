using tasks_cli.model;
using tasks_cli.utils;

namespace tasks_cli.controller;

public class TasksController
{
    public static void AddTask(string? taskDescription)
    {
        if(string.IsNullOrEmpty(taskDescription))
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

    public static void ListTasks()
    {
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            TodoTablePrinter.PrintTable(todos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }

    public static void UpdateTask(int id, string? newDescription)
    {
        if(string.IsNullOrEmpty(newDescription))
        {
            Console.WriteLine("[ERROR] No task description was provided");
            return;
        }
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            int i = todos.FindIndex(t => t.Id == id);
            if(i == -1)
            {
                Console.WriteLine($"[ERROR] Task with id {id} doesn't exist");
                return;
            }

            todos[i].Description = newDescription;
            todos[i].UpdatedAt = DateTime.Now;
            JsonController.SaveTasksInJson(todos, todoDTO.LastId);
            Console.WriteLine($"Task updated successfully (ID: {id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }

    public static void DeleteTask(int id)
    {
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            int i = todos.FindIndex(t => t.Id == id);
            if(i == -1)
            {
                Console.WriteLine($"[ERROR] Task with id {id} doesn't exist");
                return;
            }
            todos.RemoveAt(i);
            JsonController.SaveTasksInJson(todos, todoDTO.LastId);
            Console.WriteLine($"Task deleted successfully (ID: {id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }
}