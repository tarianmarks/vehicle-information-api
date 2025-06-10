using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public class VehicleInformationRepository : IVehicleInformationRepository
    {
        public async Task<DataLayer.Models.VehicleInformation> GetVehicleInformationByVIN(string vin)
        {
            return new DataLayer.Models.VehicleInformation()
            {
                DealerId = 12345,
                VIN = "14LAKDF2Q3231",
                ModifiedDate = DateTime.Now
            };
        }
        public async Task<List<DataLayer.Models.VehicleInformation>> GetAllVehicles()
        {
            return new List<DataLayer.Models.VehicleInformation>()
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
