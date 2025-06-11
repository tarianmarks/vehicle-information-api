using Microsoft.AspNetCore.Mvc;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileReaderController(IReadFromCsv csvReader, ILogger<VehicleInformationController> logger) : ControllerBase
    {
        private readonly IReadFromCsv _csvReader = csvReader;
        private readonly ILogger<VehicleInformationController>? _logger = logger;

        [HttpGet("/readFile/{csvFile}")]
        public IEnumerable<VehicleInformation> Get(string csvFile)
        {
            return _csvReader.ReadFile(csvFile);
        }
    }
}
