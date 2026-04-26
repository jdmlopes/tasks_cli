using System.CommandLine;

RootCommand root = new RootCommand("Task tracker that runs on the terminal");

Argument<string> defaultArgument = new("def")
{
    Description = "TODO: build a cli task tracker",
    DefaultValueFactory = result => "I don't do anything yet"
};

root.Arguments.Add(defaultArgument);

root.SetAction(parseResult => Console.WriteLine(parseResult.GetValue(defaultArgument)));

ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();


