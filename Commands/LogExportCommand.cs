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
    public void OnExecute(LogService logService)
    {
        AnsiConsole.WriteLine("Log export!");
    }
}
