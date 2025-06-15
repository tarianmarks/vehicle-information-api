using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public class VehicleInformationRepository(DataContext dataContext, IConfiguration configuration, ILogger<VehicleInformationRepository> logger) : IVehicleInformationRepository
    {
        private DataContext _dataContext = dataContext;
        private IConfiguration _configuration = configuration;
        private ILogger _logger = logger;

        public async Task<List<VehicleInformation>> GetAllVehicles()
        {
            using (var context = _dataContext)
            {
                return await _dataContext.VehicleInformations.ToListAsync();
            }
        }

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

        public async Task<bool> StoreExtendedVehicleInformation(List<VehicleInformationExtended> vehicleInformationExtended)
        {
            using (var context = _dataContext)
            {
                foreach (var v in vehicleInformationExtended)
                {
                    _dataContext.VehicleInformationsExtended.Add(v!);
                }

                await _dataContext.SaveChangesAsync();

                return true;
            }
        }
    }
}