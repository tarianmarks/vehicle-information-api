using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInformationController(IVehicleInformationService vehicleInformationService, ILogger<VehicleInformationController> logger) : ControllerBase
    {
        private readonly ILogger<VehicleInformationController>? _logger = logger;
        private readonly IVehicleInformationService? _vehicleInformationService = vehicleInformationService;

        //// GET: api/<vehicle-information>
        //[HttpGet]
        //public IEnumerable<VehicleInformation> Get()
        //{
        //    return new List<VehicleInformation>();
        //}

        //// GET api/<vehicle-information>/5
        //[HttpGet("{id}")]
        //public VehicleInformation Get(int id)
        //{
        //    return new VehicleInformation();
        //}

        //// POST api/<vehicle-information>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<vehicle-information>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // GET api/<vehicle-information>/<vin>/string
        [HttpGet("/vin/{vin}")]
        //public async Task<VehicleInformationAPI.Models.VehicleInformation> GetVehicleInformationByVIN(string vin)
        public async Task<IActionResult> GetVehicleInformationByVIN(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                //throw new ArgumentNullException(nameof(vin));
                return BadRequest();
            }
            var result = await _vehicleInformationService!.GetVehicleInformationByVIN(vin);

            return Ok(result);
            //return await _vehicleInformation.GetVehicleInformationByVIN(vin);
        }

        [HttpPost("")]
        public async Task<IActionResult> GetListOfVehicleInformation(PaginationFilterRequest request)
        {
            var result = await _vehicleInformationService.GetListOfVehicleInformation(request);
            return Ok(result);
        }

        [HttpGet("/batch/vin/")]
        public async Task<IActionResult> GetExtendedVehicleInformation()
        {
            var vehicles = await _vehicleInformationService.GetExtendedVehicleInformation();

            return Ok(vehicles);
        }

        [HttpPost("vehicleinformation/population/{csvFile}")]
        public async Task<IActionResult> PopulateVehicleInformation(string csvFile)
        {
            var completed = _vehicleInformationService.PopulateVehicleInformation(csvFile);

            return Ok();
        }
    }
}
