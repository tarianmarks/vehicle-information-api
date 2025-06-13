using Azure.Storage.Blobs;
using VehicleInformationAPI.BusinessLayer.Interfaces;

namespace VehicleInformationAPI.BusinessLayer
{
    public class SaveToFileStorage : ISaveToFileStorage
    {
        private readonly string _blobConnection;
        private readonly string blobContainer = "vehiclefiles";
        private readonly IConfiguration _configuration;
        private readonly ILogger<SaveToFileStorage> _logger;

        public SaveToFileStorage(IConfiguration configuration, ILogger<SaveToFileStorage> logger)
        {
            _configuration = configuration;
            _blobConnection = _configuration.GetConnectionString("AzureStorage")!;
            _logger = logger;
        }
        public async Task UploadFile(byte[] fileContent, string filename)
        {
            var blobServiceClient = new BlobServiceClient(_blobConnection);
            var containerServiceClient = blobServiceClient.GetBlobContainerClient(blobContainer);
            var blobClient = containerServiceClient.GetBlobClient(filename);

            try
            {
                using (var stream = new MemoryStream(fileContent))
                {
                    await blobClient.UploadAsync(stream, true);
                }
            } catch (Exception ex) {
                _logger.LogError($"Unable to upload the file {filename}: {ex}");
                throw new Exception($"Unable to upload the file {filename}: {ex}");
            }
        }
    }
}