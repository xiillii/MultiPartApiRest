using documentsapi.logic;
using documentsapi.Models;

namespace documentsapi.Mappers;

public static class FileUploadExtensions
{
    public static async Task<FilesToStore> CopyToAsync(this FileUploadModel model)
    {
        var response = new FilesToStore();

        response.Contract = model.ContractIdentifier;
        response.Files = new List<TheFile>();
        foreach (var file in model.Files) {
            var theFile = new TheFile() { Name = file.FileName, ContentType = file.ContentType };
            theFile.File = new MemoryStream();
            await file.CopyToAsync(theFile.File);
            theFile.File.Position = 0;

            response.Files.Add(theFile);
        }

        return response;
    }

    public static FileResponseModel CopyToResponse(this FilesToStore files)
    {
        var response = new FileResponseModel();

        response.Contract = files.Contract;
        response.Files = new List<FileModel>();
        foreach (var file in files.Files)
        {
            var theFile = new FileModel()
            {
                Name = file.Name,
                ContentType = file.ContentType,
                StorageId = file.StorageIdentifier
            };
            response.Files.Add(theFile);    
        }

        return response;
    }
}
