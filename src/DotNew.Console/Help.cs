using System.Reflection;

namespace DotNew.Console;

internal class Help
{
    public static void ShowHelp(string[] args)
    {
        if (args.Length is 0 or > 1) ShowList();
        ShowHelpFor(args[0]);
    }

    private static void ShowList()
    {
        var categories = typeof(Help).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(f => f is { IsLiteral: true, IsInitOnly: false } && f.FieldType == typeof(string))
            .Select(x => x.Name.ToLowerInvariant())
            .ToList();
        System.Console.WriteLine($"Usage: help [{string.Join('|', categories)}]");
        Environment.Exit(0);
    }

    private static void ShowHelpFor(string category)
    {
        var matches = typeof(Help).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(f => f is { IsLiteral: true, IsInitOnly: false } && f.FieldType == typeof(string))
            .Where(x => string.Equals(x.Name, category, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        if (!matches.Any())
        {
            System.Console.Error.WriteLine($"No help listed for {category}.");
            Environment.Exit(1);
        }

        System.Console.WriteLine();
        System.Console.WriteLine(matches.First().GetValue(null));
        Environment.Exit(0);
    }

    private const string Run = """
                               run - Search recursively for a single project, and run if one (and only one) is found. Does not support any extra flags, including passing args to the invoked binary via '-- [args]'. 
                               
                               run [options] - Fall-through to dotnet run
                               """;
}
