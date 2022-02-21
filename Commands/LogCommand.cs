using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using VideoCheck.Services;

namespace VideoCheck.Commands;

[Command(Name = "log",
        FullName = "Log",
        Description = "Manages the log of scanned files.")]
[HelpOption]
[Subcommand(typeof(LogClearCommand), typeof(LogExportCommand))]
public class LogCommand
{
    private readonly LogService _logService;

    public LogCommand(LogService logService)
    {
        _logService = logService;
    }

    public void OnExecute(IConsole console)
    {
        AnsiConsole.WriteLine("Log root!");
    }
}
