using LiteDB;

namespace VideoCheck.Models;

public class Scan
{
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();

    public string FilePath { get; set; } = string.Empty;
    public bool HasError { get; set; }
    public string? Error { get; set; }
}
