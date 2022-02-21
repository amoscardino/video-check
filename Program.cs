using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VideoCheck.Commands;
using VideoCheck.Services;
using Spectre.Console;

try
{
    var builder = Host.CreateDefaultBuilder();

    builder.ConfigureServices((context, services) =>
    {
        services.AddTransient<FileService>();
        services.AddTransient<LogService>();
        services.AddTransient<ScanService>();
    });

    await builder.RunCommandLineApplicationAsync<MainCommand>(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
}