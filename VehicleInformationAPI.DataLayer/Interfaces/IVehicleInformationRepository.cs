using VehicleInformationAPI.DataLayer.Models;
using VehicleInformationAPI.Models;

namespace VehicleInformationAPI.DataLayer.Interfaces
{
    public interface IVehicleInformationRepository
    {
        public Task<List<VehicleInformation>> GetAllVehicles();
        public Task<VehicleInformation> GetVehicleInformationByVIN(string vin);
    }
}
