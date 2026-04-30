using tasks_cli.model;
using tasks_cli.utils;

namespace tasks_cli.controller;

public class TasksController
{
    public static void AddTask(string? taskDescription)
    {
        if (string.IsNullOrEmpty(taskDescription))
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
                Status = TodoStatus.pending
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

    public static void ListTasks(bool filterDone, bool filterInProgress, bool filterPending, bool sortLatest)
    {
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            List<TodoStatus> filters = new();
            if(filterDone) filters.Add(TodoStatus.done);
            if(filterInProgress) filters.Add(TodoStatus.inProgress);
            if(filterPending) filters.Add(TodoStatus.pending);
            todos = todos.Where(t => filters.Contains(t.Status)).ToList();
            if(sortLatest)
                todos = todos.OrderByDescending(t => t.UpdatedAt).ToList();
            TodoTablePrinter.PrintTable(todos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }

    public static void GetTaskById(int id)
    {
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            Todo? todo = todos.Find(t => t.Id == id);
            if (todo is null)
            {
                Console.WriteLine($"[ERROR] Task with id {id} doesn't exist");
                return;
            }
            Console.WriteLine(todo.ToString());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }

    public static void UpdateTask(int id, string? newDescription)
    {
        if (string.IsNullOrEmpty(newDescription))
        {
            Console.WriteLine("[ERROR] No task description was provided");
            return;
        }
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            int i = todos.FindIndex(t => t.Id == id);
            if (i == -1)
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
            if (i == -1)
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

    public static void UpdateTaskStatus(int id, TodoStatus status)
    {
        if (!Enum.IsDefined(status))
        {
            Console.WriteLine("[ERROR] This status doesn't exist");
            return;
        }
        try
        {
            TodoDTO todoDTO = JsonController.GetTasksFromJson();
            List<Todo> todos = todoDTO.Todos;
            int i = todos.FindIndex(t => t.Id == id);
            if (i == -1)
            {
                Console.WriteLine($"[ERROR] Task with id {id} doesn't exist");
                return;
            }

            todos[i].Status = status;
            todos[i].UpdatedAt = DateTime.Now;
            JsonController.SaveTasksInJson(todos, todoDTO.LastId);
            Console.WriteLine($"Task Status updated successfully (ID: {id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
        }
    }
}