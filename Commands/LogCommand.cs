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
    public void OnExecute(LogService logService)
    {
        AnsiConsole.WriteLine("Log root!");
    }
}
