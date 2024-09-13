using System.Diagnostics;

namespace DotNew.Console;

internal class Install
{
    public static void VerifyDotnetInstalled(string[] args)
    {
        if (args.Length != 0) Help.ShowHelp(["install"]);

#if WINDOWS
        string url = "https://raw.githubusercontent.com/dotnet/install-scripts/main/src/dotnet-install.ps1";
        string outputPath = Path.Join(Environment.ExpandEnvironmentVariables("%TEMP%"), "dotnet-install.ps1");
#else
        string url = "https://raw.githubusercontent.com/dotnet/install-scripts/main/src/dotnet-install.sh";
        string outputPath = "/tmp/dotnet-install.sh";
#endif

        DownloadFile(url, outputPath);

#if !WINDOWS 
        Process? chmodProcess = Process.Start("/usr/sbin/chmod", $"+x \"{outputPath}\"");
        if (chmodProcess is null)
        {
            System.Console.Error.WriteLine($"Could not start /usr/sbin/chmod for +x on {outputPath}");
            Environment.Exit(1);
        }

        chmodProcess.WaitForExit();
        if (chmodProcess.ExitCode != 0) 
        {
            System.Console.Error.WriteLine($"Could not chmod +x {outputPath}");
            Environment.Exit(1);
        }
#endif

        Process? process = Process.Start(outputPath);
        if (process is null)
        {
            System.Console.Error.WriteLine($"Could not start {outputPath}");
            Environment.Exit(1);
        }

        process.WaitForExit();
        Environment.Exit(process.ExitCode);

    }

    private static void DownloadFile(string url, string outputPath)
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();

        using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
        response.Content.CopyToAsync(fileStream).Wait();
    }
}
