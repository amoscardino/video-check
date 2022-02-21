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
    private readonly LogService _logService;
    private readonly ScanService _scanService;

    public ScanCommand(LogService logService, ScanService scanService)
    {
        _logService = logService;
        _scanService = scanService;
    }

    public void OnExecute(IConsole console)
    {
        AnsiConsole.WriteLine("Scan root!");
    }
}
