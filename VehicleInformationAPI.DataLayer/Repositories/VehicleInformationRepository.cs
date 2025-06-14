using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public class VehicleInformationRepository(DataContext dataContext, IConfiguration configuration) : IVehicleInformationRepository
    {
        private DataContext _dataContext = dataContext;
        private IConfiguration _configuration = configuration;

        public async Task<VehicleInformation> GetVehicleInformationByVin(string vin)
        {
            using (var context = _dataContext)
            {
                var vehicle = await _dataContext.VehicleInformations
                    .Where(b => b.vin == vin).SingleOrDefaultAsync();

                return new VehicleInformation()
                {
                    dealer_Id = !string.IsNullOrEmpty(vehicle?.dealer_Id) ? vehicle.dealer_Id : string.Empty,
                    vin = !string.IsNullOrEmpty(vehicle?.vin) ? vehicle.vin : string.Empty,
                    modified_date = !string.IsNullOrEmpty(vehicle?.modified_date.ToString()) ? vehicle.modified_date : DateTime.Now,
                };
            }
        }
        public async Task<List<VehicleInformation>> GetAllVehicles()
        {
            using (var context = _dataContext)
            {
                return await _dataContext.VehicleInformations.ToListAsync();
            //    return new List<VehicleInformation>()
            //    {
            //    new VehicleInformation()
            //    {
            //        DealerId = "12345",
            //        Vin = "14LAKDF2Q3231",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "12345",
            //        Vin = "1G1ZT53826F109149",
            //        ModifiedDate = DateTime.Parse("2022-11-23")
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "12345",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "222341",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "22243",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "98765",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "98765",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "556678",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "6789",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    },

            //    new VehicleInformation()
            //    {
            //        DealerId = "6789",
            //        Vin = "14LAKDF2Q3231ERMEW325A",
            //        ModifiedDate = DateTime.Now
            //    }
            //};
            }
        }

        public async Task<bool> StoreVehicleInformation(List<VehicleInformation> vehicleInformation)
        {
            using (var context = _dataContext)
            {
                foreach (var v in vehicleInformation)
                {
                    _dataContext.VehicleInformations.Add(v!);
                }

                await _dataContext.SaveChangesAsync();

                return true;
            }
        }
    }
}