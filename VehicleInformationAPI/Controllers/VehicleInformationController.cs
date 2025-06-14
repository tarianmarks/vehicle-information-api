using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleInformationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class VehicleInformationController(IVehicleInformationService vehicleInformationService, ILogger<VehicleInformationController> logger) : ControllerBase
    {
        private readonly ILogger<VehicleInformationController>? _logger = logger;
        private readonly IVehicleInformationService? _vehicleInformationService = vehicleInformationService;

        /// <summary>
        /// Gets a vehicle and its details by passing in the Vehicle Information Number
        /// </summary>
        /// <param name="vin"></param>
        /// <returns>IActionResult with the vehicle information</returns>
        [HttpGet("vin/{vin}")]
        public async Task<IActionResult> GetVehicleInformationByVin(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                //throw new ArgumentNullException(nameof(vin));
                return BadRequest();
            }
            var result = await _vehicleInformationService!.GetVehicleInformationByVin(vin);

            return Ok(result);
            //return await _vehicleInformation.GetVehicleInformationByVIN(vin);
        }

        /// <summary>
        /// Gets a list of potentially paginated vehicles and details.  Can filter the results by ModifiedDate and DealerId
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult with the list of vehicles and details</returns>
        [HttpPost("")]
        public async Task<IActionResult> GetListOfVehicleInformation(PaginationFilterRequest request)
        {
            var result = await _vehicleInformationService.GetListOfVehicleInformation(request);
            return Ok(result);
        }

        /// <summary>
        /// Gets the details of vehicles from the NHTSA by passing in a string of concatenated Vehicle Information Numbers
        /// </summary>
        /// <param></param>
        /// <returns>IActionResult with the list of vehicles and details from NHTSA, concatenated with records from db.</returns>

        [HttpGet("nhtsa/batch/vin/")]
        public async Task<IActionResult> GetExtendedVehicleInformation()
        {
            var vehicles = await _vehicleInformationService.GetExtendedVehicleInformation();

            return Ok(vehicles);
        }

        /// <summary>
        /// Gets the details of vehicles from a csv file, saves them to the db, gets vehicles from nhtsa,
        /// then saves the combined records to the database.
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns>IActionResult status</returns>
        [HttpPost("population/{csvFile}")]
        public async Task<IActionResult> PopulateVehicleInformation(string csvFile)
        {
            var completed = await _vehicleInformationService.PopulateVehicleInformation(csvFile);

            return Ok();
        }
    }
}
