
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace documentsapi.logic;

public class AzureStorageAccount : IStorageRepository
{
    private readonly string _storageAccountName;
    private readonly string _accessKey;
    private readonly string _containerBase;

    public AzureStorageAccount(string storageAccountName, string accessKey, string containerBase)
    {
        _storageAccountName = storageAccountName;
        _accessKey = accessKey;
        _containerBase = containerBase;
    }

    public async Task<FilesToStore> UploadAsync(FilesToStore filesToStore)
    {
        var connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
            _storageAccountName, // your storage account name
            _accessKey);

        var storageAccount = CloudStorageAccount.Parse(connectionString);

        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(_containerBase);

        var sasConstraints = new SharedAccessBlobPolicy();
        sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10);
        sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Create;

        foreach (var file in filesToStore.Files) 
        { 
            
            var blob = container.GetBlockBlobReference($"{filesToStore.Contract}/{file.Name}");
            var cloudBlockBlob = new CloudBlockBlob(new Uri($"{blob.Uri}{blob.GetSharedAccessSignature(sasConstraints)}"));

            cloudBlockBlob.Metadata.Add("contract", filesToStore.Contract);
            cloudBlockBlob.Properties.ContentType = file.ContentType;
            
            await cloudBlockBlob.UploadFromStreamAsync(file.File);

            file.StorageIdentifier = blob.Uri.ToString();
        }

        return filesToStore;

    }
}
