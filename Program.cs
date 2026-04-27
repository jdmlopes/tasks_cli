using System.CommandLine;
using tasks_cli.controller;
using tasks_cli.model;

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
Command listTasks = new Command("list", "List all tasks");
listTasks.Aliases.Add("ls");
listTasks.SetAction(result => TasksController.ListTasks());
root.Subcommands.Add(listTasks);

// Show task command
Command getTask = new Command("get", "Display the task details of the informed task id");
getTask.Aliases.Add("see");
getTask.Aliases.Add("show");
Argument<int> idToGet = new("get-id")
{
    Description = "Id of the task that will be displayed"
};
getTask.Arguments.Add(idToGet);
getTask.SetAction(result => TasksController.GetTaskById(
    result.GetValue(idToGet)
));
root.Subcommands.Add(getTask);

//Update Task Command
Command updateTask = new Command("update", "Update a task with a new description");
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

//Update Status Task Command
Command updateStatus = new Command("status", "Marks a task status as done, pending or in progress");
updateStatus.Aliases.Add("stat");
Argument<int> idToUpdateStatus = new("update-id")
{
    Description = "Id of the task that will be updated"
};
Option<bool> markDone = new("--done", "-d")
{
    Description = "Mark task as done"
};
Option<bool> markInProgress = new("--inprogress", "-ip")
{
    Description = "Mark task as in progress"
};
Option<bool> markPending = new("--pending", "-p")
{
    Description = "Mark task as pending"
};
updateStatus.Arguments.Add(idToUpdateStatus);
updateStatus.Options.Add(markDone);
updateStatus.Options.Add(markInProgress);
updateStatus.Options.Add(markPending);
updateStatus.Validators.Add(result =>
{
    int count = 0;
    if (result.GetValue(markDone))
        count++;
    if (result.GetValue(markInProgress))
        count++;
    if (result.GetValue(markPending))
        count++;

    if(count == 0)
        result.AddError("[ERROR] one option must be selected (--pending, --inprogress or --done)");
    if(count > 1)
        result.AddError("[ERROR] too many options selected, choose only one (--pending, --inprogress or --done)");
});
updateStatus.SetAction(result => TasksController.UpdateTaskStatus(
    result.GetValue(idToUpdateStatus),
    result.GetValue(markDone) ? TodoStatus.done :
    result.GetValue(markInProgress) ? TodoStatus.inProgress :
    TodoStatus.pending
));
root.Subcommands.Add(updateStatus);

//Delete Task Command
Command deleteTask = new Command("delete", "Update a task with a new description");
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


