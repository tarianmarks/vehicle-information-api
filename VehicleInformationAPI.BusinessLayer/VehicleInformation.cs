using VehicleInformationAPI.BusinessLayer.Interfaces;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.BusinessLayer
{
    public class VehicleInformation(ILogger<IVehicleInformation> logger) : IVehicleInformation
    {
        private readonly ILogger<IVehicleInformation> _logger = logger;

        public VehicleInformation? GetVehicleInformation(string vin)
        {
            return null;
        }

        /// <summary>
        /// Stores vehicle information into data store
        /// </summary>
        /// <returns></returns>
        public async Task<VehicleInformation> StoreVehicleInDataStore(VehicleInformation vehicle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stores vehicle information into data store
        /// </summary>
        /// <returns>The vehicle information found with the VIN</returns>
        public async Task<Models.VehicleInformation> GetVehicleInformationByVIN(string vin)
        {
            throw new NotImplementedException();
        }
    }
}
