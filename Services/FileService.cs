namespace VideoCheck.Services;

public class FileService
{
    private static readonly string[] VIDEO_EXTENSIONS = new[] { ".mp4", ".mkv", ".m4v", ".avi" };

    public List<string> GetFiles(string inputDirectory, bool recurse = false)
    {
        var files = new List<string>();
        var attributes = File.GetAttributes(inputDirectory);

        // Check if the input is a directory or a file
        if (attributes.HasFlag(FileAttributes.Directory))
        {
            var searchOption = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            files.AddRange(Directory.EnumerateFiles(inputDirectory, "*.*", searchOption));
        }
        else
        {
            // If the input is not a directory, it must be a file and we'll treat it as the only file
            files.Add(inputDirectory);
        }

        // Be sure to filter out non-video files and ignore files that start with a .
        return files
            .Where(path => !Path.GetFileName(path).StartsWith('.'))
            .Where(path => VIDEO_EXTENSIONS.Contains(Path.GetExtension(path)))
            .OrderBy(path => path)
            .ToList();
    }
}
