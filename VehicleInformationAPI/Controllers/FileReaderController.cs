using Microsoft.AspNetCore.Mvc;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileReaderController(IReadFromCsv csvReader, ILogger<FileReaderController> logger) : ControllerBase
    {
        private readonly IReadFromCsv _csvReader = csvReader;
        private readonly ILogger<FileReaderController> _logger = logger;

        [HttpGet("/readFile/{csvFile}")]
        public async Task<IEnumerable<VehicleInformation>> Get(string csvFile)
        {
            if(string.IsNullOrEmpty(csvFile))
            {
                _logger.LogError($"Can't upload empty file");
                throw new ArgumentException($"Can't upload empty file");
            }
            
            return await _csvReader.ReadFile(csvFile);
        }
    }
}
