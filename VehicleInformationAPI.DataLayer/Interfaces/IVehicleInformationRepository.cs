using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer.Repositories
{
    public interface IVehicleInformationRepository
    {
        public Task<List<DataLayer.Models.VehicleInformation>> GetAllVehicles();
        public Task<DataLayer.Models.VehicleInformation> GetVehicleInformationByVIN(string vin);
    }
}
