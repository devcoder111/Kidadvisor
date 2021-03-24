using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Models;
using KidAdvisor.Services;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KidAdvisor.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public DataAccess _data = new DataAccess();
        private readonly string connectionString = "UseDevelopmentStorage=true";
        private readonly string containerName = "mycontainer";
        private CloudBlobContainer _container;
        private static Random random = new Random();

        private CloudStorageAccount _storageAccount;
        private CloudStorageAccount StorageAccount
        {
            get
            {
                if (_storageAccount == null)
                {
                    CloudStorageAccount.TryParse(
                        connectionString, out _storageAccount);
                }
                return _storageAccount;
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                if (Request.Form.Files != null && Request.Form.Files.Count() > 0)
                {
                    for (int i = 0; i < Request.Form.Files.Count(); i++)
                    {
                        var file = Request.Form.Files[i];
                        var folderName = Path.Combine("Resources", "Images");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            var guid = await UploadFromFileAsync(fullPath, "image/jpg"); // Upload Image
                            var fileURL = await GetReadUrlAsync(guid);

                            Image image = new Image
                            {
                                id = guid.ToString(),
                                URL = fileURL
                            };
                            var img = await _data.Create(image);
                        }
                    }
                    //var allImages = await GetListImages(); // Fetch all uploaded Images
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var allImages = await _data.GetImages(); 
                var list = allImages.Select(x => x.URL).ToList();
                return Ok(new { list });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        async private Task<CloudBlobContainer> GetContainerAsync()
        {
            if (_container != null) return _container;

            if (StorageAccount != null)
            {
                CloudBlobClient client = StorageAccount.CreateCloudBlobClient();
                _container = client.GetContainerReference(containerName);

                if (!await _container.ExistsAsync())
                    await _container.CreateAsync();
            }

            return _container;
        }

        async public Task<string> GetReadUrlAsync(string blobId, int hours = 0)
        {
            CloudBlobContainer container = await GetContainerAsync();
            if (container == null) return null;

            CloudBlockBlob blob = container.GetBlockBlobReference(blobId);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5);

            if (hours > 0)
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(hours);
            else
                sasConstraints.SharedAccessExpiryTime = DateTime.MaxValue.ToUniversalTime();

            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            string url = blob.Uri + sasBlobToken;

            return url;
        }

        async public Task<string> UploadFromFileAsync(string filePath, string contentType)
        {
            CloudBlobContainer container = await GetContainerAsync();
            if (container == null) return null;

            string guid = Guid.NewGuid().ToString();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid);
            blockBlob.Properties.ContentType = contentType;

            using (var fileStream = System.IO.File.OpenRead(filePath))
                await blockBlob.UploadFromStreamAsync(fileStream);

            return guid;
        }

        async public Task<List<string>> GetListImages()
        {
            CloudBlobContainer container = await GetContainerAsync();
            var response = await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.None, 500, null, null, null);

            List<string> imageList = new List<string>();
            foreach (var item in response.Results)
            {
                string url = await GetReadUrlAsync(item.Uri.Segments.Last());
                imageList.Add(url);
            }
            return imageList;
        }

    }
}
