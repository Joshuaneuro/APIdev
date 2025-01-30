using Azure.Storage.Blobs;
using Domain.Interfaces;

namespace DAL.Repositories
{
    public class BlobStorageRepository : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadFileAsync(string containerName, string blobName, Stream content)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, true);
        }

        public async Task<Stream> GetFileAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task DeleteFileAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
