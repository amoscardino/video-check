using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using VideoCheck.Services;

namespace VideoCheck.Commands;

[Command(Name = "scan",
        FullName = "Scan",
        Description = "Scans video files for errors")]
[HelpOption]
public class ScanCommand
{
    [Argument(0, "Input Path", "Path to scan. Defaults to current directory.")]
    public string? InputPath { get; set; }

    [Option("-m|--minutes", "Number of minutes to check for each file. Defaults to 2.", CommandOptionType.SingleOrNoValue)]
    public int Minutes { get; set; } = 2;

    [Option("-r|--recurse", "Recursive scan. Will scan folders and subfolders of the Input Path.", CommandOptionType.NoValue)]
    public bool Recurse { get; set; }

    public async Task OnExecuteAsync(ScanService scanService, LogService logService, FileService fileService)
    {
        var stopwatch = new Stopwatch();
        var inputPath = InputPath ?? Directory.GetCurrentDirectory();

        AnsiConsole.MarkupLine($"- Scanning: [bold]{inputPath}[/]");
        AnsiConsole.MarkupLine($"- Recursive: [bold]{Recurse}[/]");
        AnsiConsole.MarkupLine($"- Minutes: [bold]{Minutes}[/]");

        var filePaths = fileService.GetFiles(inputPath, Recurse);

        foreach (var filePath in filePaths)
        {
            AnsiConsole.MarkupLine($"\t{Path.GetFileName(filePath)}");

            if (logService.HasBeenScanned(filePath))
            {
                AnsiConsole.WriteLine("\t\t(already scanned)");
                continue;
            }

            stopwatch.Start();
            var error = await scanService.CheckFileAsync(inputPath, filePath, Minutes);
            stopwatch.Stop();

            if (!string.IsNullOrWhiteSpace(error))
                AnsiConsole.MarkupLine($"\t\t[red]Error![/] ({stopwatch.Elapsed.ToString("mm:ss")})");
            else
                AnsiConsole.MarkupLine("\t\t[green]Pass![/]");

            logService.LogScan(filePath, error);
        }

        AnsiConsole.MarkupLine("[green]Done![/]");

    }
}
