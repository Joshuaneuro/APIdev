using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIdev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageRepository;

        public BlobController(IBlobStorageService blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }

        [HttpPost("{containerName}/{blobName}")]
        public async Task<IActionResult> Upload(string containerName, string blobName, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            await _blobStorageRepository.UploadFileAsync(containerName, blobName, stream);
            return Ok();
        }

        [HttpGet("{containerName}/{blobName}")]
        public async Task<IActionResult> Download(string containerName, string blobName)
        {
            var stream = await _blobStorageRepository.GetFileAsync(containerName, blobName);
            return File(stream, "application/octet-stream", blobName);
        }

        [HttpDelete("{containerName}/{blobName}")]
        public async Task<IActionResult> DeleteBlob(string containerName, string blobName)
        {
            await _blobStorageRepository.DeleteFileAsync(containerName, blobName);
            return Ok();
        }
    }
}
