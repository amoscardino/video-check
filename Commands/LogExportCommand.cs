using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using VideoCheck.Services;

namespace VideoCheck.Commands;

[Command(Name = "export",
        FullName = "Export Log",
        Description = "Exports the scanned files log to disk.")]
[HelpOption]
public class LogExportCommand
{
    private readonly LogService _logService;

    public LogExportCommand(LogService logService)
    {
        _logService = logService;
    }
    
    public void OnExecute(IConsole console)
    {
        AnsiConsole.WriteLine("Log export!");
    }
}
