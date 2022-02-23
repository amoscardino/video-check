using ClosedXML.Excel;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using VideoCheck.Extensions;
using VideoCheck.Services;

namespace VideoCheck.Commands;

[Command(Name = "export",
        FullName = "Export Log",
        Description = "Exports the scanned files log to disk.")]
[HelpOption]
public class LogExportCommand
{
    [Argument(0, "Output Path", "Path to write the export file. Defaults to current directory.")]
    public string? OutputPath { get; set; }

    [Option("-a|--all", "Exports all records in the log, not just errors.", CommandOptionType.NoValue)]
    public bool IncludeAll { get; set; }

    public void OnExecute(LogService logService)
    {
        var scans = logService.ListScans(IncludeAll);

        if (scans.Count == 0)
        {
            AnsiConsole.WriteLine("No scans to export.");
            return;
        }

        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Scans");

        ws.Cell(1, 1).Value = "ID";
        ws.Cell(1, 2).Value = "File Name";
        ws.Cell(1, 3).Value = "Status";
        ws.Cell(1, 4).Value = "Path";
        ws.Cell(1, 5).Value = "Error";

        ws.Cell(2, 1).InsertData(scans.Select(x => new
        {
            ID = x.Id.ToString(),
            FileName = Path.GetFileName(x.FilePath),
            Status = x.HasError ? "Fail" : "Success",
            FullPath = x.FilePath,
            Error = x.Error.Truncate(150).Replace(Environment.NewLine, " ")
        }));

        ws.RangeUsed().AddConditionalFormat().WhenEquals("Fail").Fill.SetBackgroundColor(XLColor.Red);

        var outputPath = OutputPath ?? Directory.GetCurrentDirectory();
        var exportPath = Path.Join(outputPath, "Scans.xlsx");

        wb.SaveAs(exportPath);
    }
}
