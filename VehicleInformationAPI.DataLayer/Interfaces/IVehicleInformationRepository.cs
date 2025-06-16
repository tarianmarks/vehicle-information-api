using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer.Interfaces
{
    public interface IVehicleInformationRepository
    {
        public Task<List<VehicleInformation>> GetAllVehicles();
        public Task<VehicleInformation> GetVehicleInformationByVin(string vin);

        public Task<bool> StoreVehicleInformation(List<VehicleInformation> vehicleInformation);
    }
}
