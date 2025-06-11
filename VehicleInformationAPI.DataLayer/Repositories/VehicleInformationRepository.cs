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
                    VIN = "1G1ZT53826F109149",
                    ModifiedDate = DateTime.Parse("2022-11-23")
                },

                new VehicleInformation()
                {
                    DealerId = 12345,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 222341,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 22243,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 98765,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 98765,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 556678,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 6789,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                },

                new VehicleInformation()
                {
                    DealerId = 6789,
                    VIN = "14LAKDF2Q3231ERMEW325A",
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public void StoreVehicleInformation(List<VehicleInformation> vehicleInformation)
        {
            //connect to data store
            
            //save
            throw new NotImplementedException();
        }
    }
}
