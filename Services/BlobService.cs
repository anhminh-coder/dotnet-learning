using Azure.Storage.Blobs;

public class BlobService
{
    private readonly string _connectionString = "";

    public BlobService(IConfiguration config)
    {
        _connectionString = config["AzureBlob:ConnectionString"];
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient("images");

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, true);

        return blobClient.Uri.ToString();
    }
}