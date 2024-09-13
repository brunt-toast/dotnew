using System.Diagnostics;

namespace DotNew.Console;

internal static class DotNetCommand
{
    public static void Invoke(string command, string[] args)
    {
        args = [command, ..args];

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = string.Join(' ', args),
            CreateNoWindow = true
        };

        Process? process = Process.Start(psi);

        if (process is null)
        {
            System.Console.Error.WriteLine($"Could not start: Process spawning 'dotnet' with args '{string.Join(' ', args)}' returned null. Is dotnet installed?");
            Environment.Exit(1);
            return;
        }

        process.WaitForExit();
        Environment.Exit(process.ExitCode);
    }
}
