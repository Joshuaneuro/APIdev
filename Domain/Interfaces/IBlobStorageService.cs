using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBlobStorageService
    {
        Task UploadFileAsync(string containerName, string blobName, Stream fileStream);
        Task<Stream> GetFileAsync(string containerName, string blobName);
        Task DeleteFileAsync(string containerName, string blobName);
    }
}
