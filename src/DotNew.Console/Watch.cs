using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNew.Console;
internal class Watch
{
    public static void WatchProject(string[] args)
    {
        if (args.Length == 0)
        {
            WatchAny();
        }
        else
        {
            DotNetCommand.Invoke("watch", args);
        }
    }

    private static void WatchAny()
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
                DotNetCommand.Invoke("watch", ["--project", projects[0]]);
                break;
        }
    }
}
