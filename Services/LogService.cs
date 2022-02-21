
using LiteDB;
using Spectre.Console;
using VideoCheck.Models;
using static System.Environment;

namespace VideoCheck.Services;

public class LogService : IDisposable
{
    private const string DIR_NAME = "video-check";
    private const string FILE_NAME = "video-check.db";

    private readonly LiteDatabase _database;

    public LogService()
        => _database = new LiteDatabase(GetDatabasePath());

    public void Dispose()
        => _database.Dispose();

    public List<Scan> ListScans(bool includeSuccess)
    {
        var collection = GetCollection();

        return collection.Query()
            .Where(x => includeSuccess || x.HasError)
            .OrderBy(x => x.FilePath)
            .ToList();
    }

    public bool HasBeenScanned(string path)
    {
        var collection = GetCollection();

        return collection.Query().Where(x => x.FilePath == path).Count() > 0;
    }

    public void LogScan(string path, string error)
    {
        var collection = GetCollection();

        collection.Insert(new Scan
        {
            FilePath = path,
            HasError = !string.IsNullOrWhiteSpace(error),
            Error = !string.IsNullOrWhiteSpace(error) ? error : null
        });
    }

    public void ClearScans()
    {
        var collection = GetCollection();

        collection.DeleteAll();
    }

    private ILiteCollection<Scan> GetCollection()
    {
        var collection = _database.GetCollection<Scan>("scans");

        collection.EnsureIndex(x => x.FilePath, true);

        return collection;
    }

    private string GetDatabasePath()
    {
        // Use DoNotVerify in case LocalApplicationData doesn’t exist.
        var appData = Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.DoNotVerify), DIR_NAME);

        // Ensure the directory and all its parents exist.
        var dir = Directory.CreateDirectory(appData);

        return Path.Combine(appData, FILE_NAME);
    }
}
