using Domain.Interfaces;

namespace APIdev.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobStorageService _blobStorageRepository;

        public BlobStorageService(IBlobStorageService blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }

        public async Task UploadFileAsync(string containerName, string blobName, Stream fileStream)
        {
            // Add any application-specific logic here, if needed
            await _blobStorageRepository.UploadFileAsync(containerName, blobName, fileStream);
        }

        public async Task<Stream> GetFileAsync(string containerName, string blobName)
        {
            return await _blobStorageRepository.GetFileAsync(containerName, blobName);
        }

        public async Task DeleteFileAsync(string containerName, string blobName)
        {
            await _blobStorageRepository.DeleteFileAsync(containerName, blobName);
        }
    }
}
