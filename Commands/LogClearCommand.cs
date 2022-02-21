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
    private readonly LogService _logService;

    public LogClearCommand(LogService logService)
    {
        _logService = logService;
    }
    
    public void OnExecute(IConsole console)
    {
        AnsiConsole.WriteLine("Log clear!");
    }
}
