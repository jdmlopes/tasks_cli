using System.CommandLine;
using tasks_cli.controller;

RootCommand root = new RootCommand("Task tracker that runs on the terminal");



Command addTask = new Command("add", "Adds a new task to the list");

Argument<string> taskDescription = new("task-description")
{
    Description = "Description of the task"
};

addTask.Arguments.Add(taskDescription);
addTask.SetAction(result => TasksController.AddTask(result.GetValue(taskDescription)));

root.Subcommands.Add(addTask);

ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();


