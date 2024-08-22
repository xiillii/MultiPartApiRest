namespace documentsapi.Models;

public class FileUploadModel
{
    public List<IFormFile> Files { get; set; }
    public string ContractIdentifier { get; set; }    
}
