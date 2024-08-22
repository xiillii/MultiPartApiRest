namespace documentsapi.logic;

public class TheFile
{
    public MemoryStream File { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? StorageIdentifier { get; set; }
    public string? ContentType { get; set; }
}
