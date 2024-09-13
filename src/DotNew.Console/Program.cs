namespace DotNew.Console;

internal class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            System.Console.WriteLine("Usage: dotnew [--help | args]");
            Environment.Exit(0);
        }

        if (args[0] == "--help" || args[0] == "-h" || args[0] == "-?" || args[0] == "help")
        {
            Help.ShowHelp(args[1..]);
        }

        if (args.Length >= 2 && (args[1] == "--help" || args[1] == "-h" || args[1] == "-?" || args[1] == "help"))
        {
            Help.ShowHelp([args[0]]);
        }

        if (args[0] == "run") Run.RunProject(args[1..]);
        if (args[0] == "install") Install.VerifyDotnetInstalled(args[1..]);
        if (args[0] == "watch") Watch.WatchProject(args[1..]);
    }
}