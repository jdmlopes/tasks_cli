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

//Update Task Command
Command updateTask = new Command("update","Update a task with a new description");
updateTask.Aliases.Add("upd");
Argument<int> idToUpdate = new("update-id")
{
    Description = "Id of the task that will be updated"
};
Argument<string> newDescription = new("new-description")
{
    Description = "New description of the task"
};
updateTask.Arguments.Add(idToUpdate);
updateTask.Arguments.Add(newDescription);
updateTask.SetAction(result => TasksController.UpdateTask(
    result.GetValue(idToUpdate),
    result.GetValue(newDescription)
));
root.Subcommands.Add(updateTask);

//Delete Task Command
Command deleteTask = new Command("delete","Update a task with a new description");
deleteTask.Aliases.Add("del");
Argument<int> idToDelete = new("delete-id")
{
    Description = "Id of the task that will be deleted"
};
deleteTask.Arguments.Add(idToDelete);
deleteTask.SetAction(result => TasksController.DeleteTask(
    result.GetValue(idToDelete)
));
root.Subcommands.Add(deleteTask);


ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();


