using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.Models;
using VehicleInformationAPI.DataLayer.Interfaces;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public class VehicleInformationRepository : IVehicleInformationRepository
    {
        public async Task<VehicleInformation> GetVehicleInformationByVIN(string vin)
        {
            return new VehicleInformation()
            {
                DealerId = 12345,
                VIN = "14LAKDF2Q3231",
                ModifiedDate = DateTime.Now
            };
        }
        public async Task<List<VehicleInformation>> GetAllVehicles()
        {
            return new List<VehicleInformation>()
            {
                new VehicleInformation()
                {
                    DealerId = 12345,
                    VIN = "14LAKDF2Q3231",
                    ModifiedDate = DateTime.Now
                },
                
                new VehicleInformation()
                {
                    DealerId = 12345,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                }
            };
        }
    }
}
