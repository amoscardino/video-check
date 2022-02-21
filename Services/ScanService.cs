using CliWrap;
using CliWrap.Buffered;
using Spectre.Console;

namespace VideoCheck.Services;

public class ScanService
{
    public async Task<string> CheckFileAsync(string inputPath, string filePath, int minutes)
    {
        var result = await Cli.Wrap("ffmpeg")
            .WithArguments(args => args
                .Add("-v").Add("error")
                .Add("-t").Add(minutes * 60)
                .Add("-i").Add(filePath)
                .Add("-f").Add("null")
                .Add("-"))
            .WithWorkingDirectory(inputPath)
            .ExecuteBufferedAsync();

        if (!string.IsNullOrWhiteSpace(result.StandardError))
            return result.StandardError;

        if (!string.IsNullOrWhiteSpace(result.StandardOutput))
            return result.StandardOutput;

        if (result.ExitCode != 0)
            return "Unknown error";

        return string.Empty;
    }
}
