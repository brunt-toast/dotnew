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

        if (args[0] == "run") Run.RunProject(args[1..]);
    }
}