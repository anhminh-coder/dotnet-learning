using Azure.Identity;
using Azure.Storage.Blobs;

public class BlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(IConfiguration config)
    {
        var accountName = config["AzureBlob:AccountName"];

        _blobServiceClient = new BlobServiceClient(
            new Uri($"https://{accountName}.blob.core.windows.net"),
            new DefaultAzureCredential()
        );
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var containerClient =
            _blobServiceClient.GetBlobContainerClient("images");

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, true);

        return blobClient.Uri.ToString();
    }
}