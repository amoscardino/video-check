using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VideoCheck.Commands;
using VideoCheck.Services;

var builder = Host
    .CreateDefaultBuilder();

builder.ConfigureServices((context, services) =>
{
    services.AddTransient<LogService>();
    services.AddTransient<ScanService>();
});

builder.RunCommandLineApplicationAsync<MainCommand>(args);
