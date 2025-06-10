using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Interfaces;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public class VehicleInformationRepository : IVehicleInformationRepository
    {
        public async Task<VehicleInformationDataObject> GetVehicleInformationByVIN(string vin)
        {
            return new VehicleInformationDataObject()
            {
                DealerId = 12345,
                VIN = "14LAKDF2Q3231",
                ModifiedDate = DateTime.Now
            };
        }
        public async Task<List<VehicleInformationDataObject>> GetAllVehicles()
        {
            return new List<VehicleInformationDataObject>()
            {
                new VehicleInformationDataObject()
                {
                    DealerId = 12345,
                    VIN = "14LAKDF2Q3231",
                    ModifiedDate = DateTime.Now
                },
                
                new VehicleInformationDataObject()
                {
                    DealerId = 12345,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                }
            };
        }
    }
}
