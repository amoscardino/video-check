namespace VideoCheck.Services;

public class FileService
{
    private static readonly string[] VIDEO_EXTENSIONS = new[] { ".mp4", ".mkv", ".m4v", ".avi" };

    public List<string> GetFiles(string inputDirectory, bool recurse = false)
    {
        var searchOption = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        return Directory
            .EnumerateFiles(inputDirectory, "*.*", searchOption)
            .Where(path => !Path.GetFileName(path).StartsWith('.'))
            .Where(path => VIDEO_EXTENSIONS.Contains(Path.GetExtension(path)))
            .ToList();
    }
}
