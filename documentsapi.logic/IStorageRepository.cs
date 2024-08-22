namespace documentsapi.logic;

public interface IStorageRepository
{
    Task<FilesToStore> UploadAsync(FilesToStore filesToStore);
}
