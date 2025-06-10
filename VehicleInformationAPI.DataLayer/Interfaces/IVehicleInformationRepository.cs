using VehicleInformationAPI.DataLayer.Models;

namespace VehicleInformationAPI.DataLayer.Interfaces
{
    public interface IVehicleInformationRepository
    {
        public Task<List<VehicleInformationDataObject>> GetAllVehicles();
        public Task<VehicleInformationDataObject> GetVehicleInformationByVIN(string vin);
    }
}
