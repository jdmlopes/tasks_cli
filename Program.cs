using System.CommandLine;
using tasks_cli.controller;

RootCommand root = new("Task tracker that runs on the terminal");


// Add Task Command
Command addTask = new("add", "Adds a new task to the list");
Argument<string> taskDescription = new("task-description")
{
    Description = "Description of the task"
};
addTask.Arguments.Add(taskDescription);
addTask.SetAction(result => TasksController.AddTask(result.GetValue(taskDescription)));
root.Subcommands.Add(addTask);

// List Tasks Command
Command listTasks = new Command("list","List all tasks");
listTasks.Aliases.Add("ls");
listTasks.SetAction(result => TasksController.ListTasks());
root.Subcommands.Add(listTasks);


ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();


