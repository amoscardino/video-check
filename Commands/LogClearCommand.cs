using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using VideoCheck.Services;

namespace VideoCheck.Commands;

[Command(Name = "clear",
        FullName = "Clear Log",
        Description = "Clears the scanned files log")]
[HelpOption]
public class LogClearCommand
{
    public void OnExecute(LogService logService)
    {
        logService.ClearScans();
        AnsiConsole.WriteLine("Cleared!");
    }
}
