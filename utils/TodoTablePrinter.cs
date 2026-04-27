using tasks_cli.model;

namespace tasks_cli.utils;
public static class TodoTablePrinter
{
    private const int IdWidth = 4;
    private const int DescWidth = 32;
    private const int StatusWidth = 15;
    private const int DateWidth = 22;

    public static void PrintTable(IEnumerable<Todo> todos)
    {
        var list = todos.ToList();

        string top    = $"┌{Bar(IdWidth)}┬{Bar(DescWidth)}┬{Bar(StatusWidth)}┬{Bar(DateWidth)}┬{Bar(DateWidth)}┐";
        string header = $"│{"ID".PadLeft(IdWidth)}│{"Description".PadRight(DescWidth)}│{"Status".PadRight(StatusWidth)}│{"Created At".PadRight(DateWidth)}│{"Updated At".PadRight(DateWidth)}│";
        string sep    = $"├{Bar(IdWidth)}┼{Bar(DescWidth)}┼{Bar(StatusWidth)}┼{Bar(DateWidth)}┼{Bar(DateWidth)}┤";
        string bottom = $"└{Bar(IdWidth)}┴{Bar(DescWidth)}┴{Bar(StatusWidth)}┴{Bar(DateWidth)}┴{Bar(DateWidth)}┘";

        var prevColor = Console.ForegroundColor;

        WriteColored(top, ConsoleColor.Cyan);
        WriteColored(header, ConsoleColor.Cyan);
        WriteColored(sep, ConsoleColor.Cyan);

        foreach (var todo in list)
        {
            var (statusIcon, statusColor) = todo.Status switch
            {
                TodoStatus.done       => ("[X] done",       ConsoleColor.DarkGreen),
                TodoStatus.inProgress => ("[-] in progress", ConsoleColor.DarkYellow),
                TodoStatus.todo       => ("[ ] pending",        ConsoleColor.DarkBlue),
                _                    => (todo.Status.ToString(), prevColor)
            };

            var desc       = Truncate(todo.Description, DescWidth).PadRight(DescWidth);
            var idStr      = todo.Id.ToString().PadLeft(IdWidth);
            var statusStr  = statusIcon.PadRight(StatusWidth);   // pad BEFORE writing
            var createdStr = todo.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss").PadRight(DateWidth);
            var updatedStr = todo.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss").PadRight(DateWidth);

            // Write each segment, applying color only to the status cell
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.Write("│");
            Console.ForegroundColor = prevColor;          Console.Write(idStr);
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.Write("│");
            Console.ForegroundColor = prevColor;          Console.Write(desc);
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.Write("│");
            Console.ForegroundColor = statusColor;        Console.Write(statusStr);
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.Write("│");
            Console.ForegroundColor = prevColor;          Console.Write(createdStr);
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.Write("│");
            Console.ForegroundColor = prevColor;          Console.Write(updatedStr);
            Console.ForegroundColor = ConsoleColor.Cyan;  Console.WriteLine("│");
        }

        WriteColored(bottom, ConsoleColor.Cyan);

        int done   = list.Count(t => t.Status == TodoStatus.done);
        int inProg = list.Count(t => t.Status == TodoStatus.inProgress);
        int pending = list.Count(t => t.Status == TodoStatus.todo);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{list.Count} tasks  |  {done} done  |  {inProg} in progress  |  {pending} pending");
        Console.ForegroundColor = prevColor;
    }

    private static string Bar(int width) => new string('─', width);

    private static string Truncate(string s, int max) =>
        s.Length <= max ? s : s[..(max - 1)] + "…";

    private static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }
}