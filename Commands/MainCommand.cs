
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace VideoCheck.Commands;

[Command(Name = "vcheck",
            FullName = "Video Check",
            Description = "Scans video files for errors.")]
[HelpOption]
[VersionOptionFromMember(MemberName = "GetVersion")]
[Subcommand(typeof(LogCommand), typeof(ScanCommand))]
public class MainCommand
{
    private void OnExecute(CommandLineApplication app)
        => app.ShowHelp();

    private string? GetVersion()
        => typeof(MainCommand)
            .Assembly?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;
}