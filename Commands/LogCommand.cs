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
    [Option("-a|--all", "Shows all records in the log, not just errors.", CommandOptionType.NoValue)]
    public bool IncludeAll { get; set; }

    public void OnExecute(LogService logService)
    {
        var scans = logService.ListScans(IncludeAll);

        if (scans.Count == 0)
        {
            AnsiConsole.WriteLine("No scans to show.");
            return;
        }

        var table = new Table();
        table.AddColumns("File Name", "Success");

        foreach (var scan in scans)
        {
            var fileName = Path.GetFileName(scan.FilePath).EscapeMarkup();
            var status = scan.HasError ? "[red]Fail[/]" : "[green]Pass[/]";

            table.AddRow(fileName, status);
        }

        AnsiConsole.Write(table);
    }
}
