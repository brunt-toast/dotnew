namespace DotNew.Console;

internal static class Run
{
    public static void RunProject(string[] args)
    {
        if (args.Length == 0)
        {
            RunAny();
        }
        else
        {
            DotNetCommand.Invoke("run", args);
        }
    }

    private static void RunAny()
    {
        string cwd = Directory.GetCurrentDirectory();
        string[] projects = Directory.GetFiles(cwd, "*.csproj", SearchOption.AllDirectories);
        
        switch (projects.Length)
        {
            case 0:
                System.Console.Error.WriteLine($"{cwd} has no descendants matching **/*.csproj");
                Environment.Exit(1);
                return;
            case > 1:
                System.Console.Error.WriteLine($"{cwd} has too many descendants matching **/*.csproj. Maybe you meant:\n{string.Join("\n\t", projects)}");
                Environment.Exit(1);
                return;
            default:
                DotNetCommand.Invoke("run", ["--project", projects[0]]);
                break;
        }
    }
}
